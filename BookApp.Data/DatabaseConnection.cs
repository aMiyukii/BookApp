using System.Data.SqlClient;

namespace DAL
{
    public class DatabaseConnection
    {
        private SqlConnection connection;

        public async Task OpenConnectionAsync()
        {
            try
            {
                connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=BookApp;");
                await connection.OpenAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error opening connection: " + ex.Message);
                throw new InvalidOperationException("Connection not working", ex);
            }
        }

        public SqlConnection GetSqlConnection()
        {
            return connection;
        }

        public async Task CloseConnectionAsync()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                await connection.CloseAsync();
            }
        }
    }
}