using BookApp.Core.Interfaces;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DAL;

namespace BookApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public UserRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection ?? throw new ArgumentNullException(nameof(databaseConnection));
        }

        public async Task<bool> LoginAsync(string emailaddress, string password)
        {
            if (string.IsNullOrWhiteSpace(emailaddress)) throw new ArgumentNullException(nameof(emailaddress));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));

            await _databaseConnection.OpenConnectionAsync();

            try
            {
                using (var connection = _databaseConnection.GetSqlConnection())
                {
                    using (var command =
                           new SqlCommand(
                               "SELECT COUNT(1) FROM [user] WHERE emailaddress = @emailaddress AND password = @password",
                               connection))
                    {
                        command.Parameters.AddWithValue("@emailaddress", emailaddress);
                        command.Parameters.AddWithValue("@password", password);

                        var result = (int)await command.ExecuteScalarAsync();
                        return result > 0;
                    }
                }
            }
            finally
            {
                await _databaseConnection.CloseConnectionAsync();
            }
        }

        public async Task<int> GetUserIdByEmailAsync(string emailaddress)
        {
            int userId = 0;
            await _databaseConnection.OpenConnectionAsync();

            try
            {
                using (var connection = _databaseConnection.GetSqlConnection())
                {
                    using (var command = new SqlCommand("SELECT id FROM [user] WHERE emailaddress = @emailaddress",
                               connection))
                    {
                        command.Parameters.AddWithValue("@emailaddress", emailaddress);
                        var result = await command.ExecuteScalarAsync();
                        if (result != null && int.TryParse(result.ToString(), out userId))
                        {
                            return userId;
                        }
                    }
                }
            }
            finally
            {
                await _databaseConnection.CloseConnectionAsync();
            }

            return userId;
        }

        public async Task<bool> CreateUserAsync(string name, string emailaddress, string password)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(emailaddress)) throw new ArgumentNullException(nameof(emailaddress));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));

            await _databaseConnection.OpenConnectionAsync();

            try
            {
                using (var connection = _databaseConnection.GetSqlConnection())
                {
                    string insertQuery =
                        "INSERT INTO [user] (name, emailaddress, password) VALUES (@name, @emailaddress, @password)";
                    using (var command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@emailaddress", emailaddress);
                        command.Parameters.AddWithValue("@password", password);

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Error in CreateUserAsync: {ex.Message}");
                throw; // Re-throw the exception to propagate it up the call stack
            }
            finally
            {
                await _databaseConnection.CloseConnectionAsync();
            }
        }

        public async Task<bool> UserExistsAsync(string emailaddress)
        {
            if (string.IsNullOrWhiteSpace(emailaddress)) throw new ArgumentNullException(nameof(emailaddress));

            await _databaseConnection.OpenConnectionAsync();

            try
            {
                using (var connection = _databaseConnection.GetSqlConnection())
                {
                    using (var command =
                           new SqlCommand("SELECT COUNT(1) FROM [user] WHERE emailaddress = @emailaddress", connection))
                    {
                        command.Parameters.AddWithValue("@emailAddress", emailaddress);

                        var result = (int)await command.ExecuteScalarAsync();
                        return result > 0;
                    }
                }
            }
            finally
            {
                await _databaseConnection.CloseConnectionAsync();
            }
        }
    }
}