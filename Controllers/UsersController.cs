using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using British_Kingdom_back.Models;

namespace British_Kingdom_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var users = new List<User>();
            var query = "SELECT * FROM Users";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                DateCreated = reader.GetString(reader.GetOrdinal("DateCreated")),

                            };
                            users.Add(user);
                        }
                    }
                }
            }

            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            int newId;

            var query = "INSERT INTO Users (Address, FirstName, LastName, PhoneNumber, DateCreated) OUTPUT INSERTED.Id VALUES (@Address, @FirstName, @LastName, @PhoneNumber, @DateCreated)";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Address", user.Address);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                    command.Parameters.AddWithValue("@DateCreated", user.DateCreated);

                    newId = (int)await command.ExecuteScalarAsync();
                }
            }

            user.Id = newId;
            return CreatedAtAction(nameof(GetAllUsers), new { id = newId }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            var query = "UPDATE Users SET Address = @Address, FirstName = @FirstName, LastName = @LastName, PhoneNumber = @PhoneNumber WHERE Id = @Id";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Address", user.Address);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return Ok(new { message = "User updated successfully" });
                    }
                    else
                    {
                        return NotFound(new { message = "User not found" });
                    }
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            var query = "DELETE FROM Users WHERE Id = @Id";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return Ok(new { message = "User deleted successfully" });
                    }
                    else
                    {
                        return NotFound(new { message = "User not found" });
                    }
                }
            }
        }

    }


}
