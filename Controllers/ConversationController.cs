// ConversationController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using British_Kingdom_back.Models;

namespace VotreProjet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConversationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetConversationsByProfilAndTicketId([FromQuery] int profilId, [FromQuery] int ticketId)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var conversations = new List<Conversation>();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM Conversation WHERE ProfilId = @ProfilId AND IdTicket = @TicketId ORDER BY DateCrea ASC", connection))
                {
                    command.Parameters.AddWithValue("@ProfilId", profilId);
                    command.Parameters.AddWithValue("@TicketId", ticketId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var conversation = new Conversation
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                ProfilId = reader.GetInt32(reader.GetOrdinal("ProfilId")),
                                IdTicket = reader.GetInt32(reader.GetOrdinal("IdTicket")),
                                DateCrea = reader.GetDateTime(reader.GetOrdinal("DateCrea")),
                                Message = reader.GetString(reader.GetOrdinal("Message")),
                                Admin = reader.GetBoolean(reader.GetOrdinal("Admin")),
                                Image = reader.GetString(reader.GetOrdinal("Image")),
                                VueUser = reader.GetBoolean(reader.GetOrdinal("VueUser")),
                                VueAdmin = reader.GetBoolean(reader.GetOrdinal("VueAdmin")),

                                
                            };

                            conversations.Add(conversation);
                        }
                    }
                }
            }

            return Ok(conversations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConversationById(int id)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM Conversation WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var conversation = new Conversation
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                ProfilId = reader.GetInt32(reader.GetOrdinal("ProfilId")),
                                IdTicket = reader.GetInt32(reader.GetOrdinal("IdTicket")),
                                DateCrea = reader.GetDateTime(reader.GetOrdinal("DateCrea")),
                                Message = reader.GetString(reader.GetOrdinal("Message")),
                                Admin = reader.GetBoolean(reader.GetOrdinal("Admin")),
                                Image = reader.GetString(reader.GetOrdinal("Image")),
                                VueUser = reader.GetBoolean(reader.GetOrdinal("VueUser")),
                                VueAdmin = reader.GetBoolean(reader.GetOrdinal("VueAdmin")),
                            };

                            return Ok(conversation);
                        }
                        else
                        {
                            return NotFound(); // La conversation avec l'ID spécifié n'a pas été trouvée.
                        }
                    }
                }
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateConversation(Conversation conversation)
        {
            if (conversation == null)
            {
                return BadRequest("Conversation data is missing.");
            }

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("INSERT INTO Conversation (ProfilId, IdTicket, DateCrea, Message, Admin, Image, VueUser, VueAdmin) OUTPUT INSERTED.Id VALUES (@ProfilId, @IdTicket, @DateCrea, @Message, @Admin, @Image, @VueUser, @VueAdmin);", connection))
                {
                    command.Parameters.AddWithValue("@ProfilId", conversation.ProfilId);
                    command.Parameters.AddWithValue("@IdTicket", conversation.IdTicket);
                    command.Parameters.AddWithValue("@DateCrea", DateTime.Now);
                    command.Parameters.AddWithValue("@Message", conversation.Message);
                    command.Parameters.AddWithValue("@Admin", conversation.Admin);
                    command.Parameters.AddWithValue("@Image", conversation.Image);
                    command.Parameters.AddWithValue("@VueUser", conversation.VueUser);
                    command.Parameters.AddWithValue("@VueAdmin", conversation.VueAdmin);

                    var newConversationId = Convert.ToInt32(await command.ExecuteScalarAsync());

                    if (newConversationId <= 0)
                    {
                        return StatusCode(500, "La création de la conversation a échoué.");
                    }

                    conversation.Id = newConversationId;

                    return CreatedAtAction(nameof(GetConversationById), new { id = newConversationId }, conversation);
                }
            }
        }

    }

}
