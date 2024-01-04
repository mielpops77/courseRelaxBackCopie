using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using British_Kingdom_back.Models;

namespace British_Kingdom_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivreOrController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LivreOrController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult CreateMessage(LivreOr livreOr)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            int newId;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO LivreOr (ProfilId, Name, Message, Validation, DateofCrea) OUTPUT INSERTED.ID VALUES (@ProfilId, @Name, @Message, @Validation, @DateofCrea)", connection))
                {
                    command.Parameters.AddWithValue("@ProfilId", livreOr.ProfilId);
                    command.Parameters.AddWithValue("@Name", livreOr.Name);
                    command.Parameters.AddWithValue("@Message", livreOr.Message);
                    command.Parameters.AddWithValue("@Validation", livreOr.Validation);
                    command.Parameters.AddWithValue("@DateofCrea", livreOr.DateofCrea); // Utiliser DateTimeOffset directement
                    newId = (int)command.ExecuteScalar();
                }
            }

            livreOr.Id = newId;
            return CreatedAtAction(nameof(GetMessageById), new { id = newId, profilId = livreOr.ProfilId }, livreOr);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessageById(int id, int profilId)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var query = "SELECT * FROM LivreOr WHERE Id = @Id AND ProfilId = @ProfilId";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@ProfilId", profilId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var livreOr = new LivreOr
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                ProfilId = reader.GetInt32(reader.GetOrdinal("ProfilId")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Message = reader.GetString(reader.GetOrdinal("Message")),
                                Validation = reader.GetBoolean(reader.GetOrdinal("Validation")),
                                DateofCrea = reader.GetDateTime(reader.GetOrdinal("DateofCrea"))
                            };

                            return Ok(livreOr);
                        }
                        else
                        {
                            return NotFound(new { message = "Message non trouvé" });
                        }
                    }
                }
            }
        }

        // Ajoutez les méthodes pour les autres actions (GetAllMessages, UpdateMessage, DeleteMessage) comme dans votre CatController

        [HttpGet]
        public async Task<IActionResult> GetAllMessages(int profilId)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var messages = new List<LivreOr>();
            string query = "SELECT * FROM LivreOr WHERE ProfilId = @ProfilId";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProfilId", profilId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var message = new LivreOr
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                ProfilId = reader.GetInt32(reader.GetOrdinal("ProfilId")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Message = reader.GetString(reader.GetOrdinal("Message")),
                                Validation = reader.GetBoolean(reader.GetOrdinal("Validation")),
                                DateofCrea = reader.GetDateTime(reader.GetOrdinal("DateofCrea"))
                            };
                            messages.Add(message);
                        }
                    }
                }
            }

            return Ok(messages);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM LivreOr WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok(new { message = "Message supprimé avec succès" });
                    }
                    else
                    {
                        return NotFound(new { message = "Message non trouvé" });
                    }
                }
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMessage(int id, LivreOr livreOr)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Assurez-vous que le message avec l'ID spécifié existe avant de le mettre à jour
                if (MessageExists(connection, id))
                {
                    // Mettez à jour le message
                    using (var command = new SqlCommand("UPDATE LivreOr SET ProfilId = @ProfilId, Name = @Name, Message = @Message, Validation = @Validation, DateofCrea = @DateofCrea WHERE Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@ProfilId", livreOr.ProfilId);
                        command.Parameters.AddWithValue("@Name", livreOr.Name);
                        command.Parameters.AddWithValue("@Message", livreOr.Message);
                        command.Parameters.AddWithValue("@Validation", livreOr.Validation);
                        command.Parameters.AddWithValue("@DateofCrea", livreOr.DateofCrea);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok(new { message = "Message mis à jour avec succès" });
                        }
                        else
                        {
                            return NotFound(new { message = "Message non trouvé" });
                        }
                    }
                }
                else
                {
                    return NotFound(new { message = "Message non trouvé" });
                }
            }
        }

        // Méthode utilitaire pour vérifier l'existence du message
        private bool MessageExists(SqlConnection connection, int id)
        {
            using (var command = new SqlCommand("SELECT COUNT(*) FROM LivreOr WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }


    }
}