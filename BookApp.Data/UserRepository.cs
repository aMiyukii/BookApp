using BookApp.Core.Interfaces;
using DAL;
using System.Data.SqlClient;

namespace BookApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public UserRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection ?? throw new ArgumentNullException(nameof(databaseConnection));
        }

        public async Task<bool> LoginAsync(string emailAddress, string password)
        {
            if (string.IsNullOrWhiteSpace(emailAddress)) throw new ArgumentNullException(nameof(emailAddress));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));

            await _databaseConnection.OpenConnectionAsync();

            try
            {
                using (var connection = _databaseConnection.GetSqlConnection())
                {
                    using (var command = new SqlCommand("SELECT COUNT(1) FROM [user] WHERE emailaddress = @EmailAddress AND password = @Password", connection))
                    {
                        command.Parameters.AddWithValue("@EmailAddress", emailAddress);
                        command.Parameters.AddWithValue("@Password", password);

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

        public async Task<int> GetUserIdByEmailAsync(string emailAddress)
        {
            int userId = 0;
            await _databaseConnection.OpenConnectionAsync();

            try
            {
                using (var connection = _databaseConnection.GetSqlConnection())
                {
                    using (var command = new SqlCommand("SELECT id FROM [user] WHERE emailaddress = @EmailAddress", connection))
                    {
                        command.Parameters.AddWithValue("@EmailAddress", emailAddress);
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

            return userId; // 0 if not found
        }
    }
}
