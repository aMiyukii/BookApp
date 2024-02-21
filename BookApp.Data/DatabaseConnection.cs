using System.Data.SqlClient;

namespace DAL
{
    public class DatabaseConnetion
    {
        private SqlConnection connection;

        public void OpenConnection()
        {
            try
            {
                connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=BookApp;");

                connection.Open();
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Connection not working");
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