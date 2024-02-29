using System.Data.SqlClient;

namespace DAL
{
    public class DatabaseConnection
    {
        private SqlConnection connection;

        public void OpenConnection()
        {
            try
            {
                connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=BookApp;");

                connection.Open();
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


        public void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}