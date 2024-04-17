using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using BookApp.Core.DTO;
using BookApp.Core.Models;

namespace BookApp.Data
{
    public class BookRepository
    {
        public List<BookDTO> GetAll()
        {
            BookDTO bookDTO = new BookDTO();
            
            List<BookDTO> books = new List<BookDTO>();
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.OpenConnection();
            
            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string selectQuery = "SELECT id, title, author, image FROM dbo.book";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["id"]);
                                string title = reader["title"].ToString();
                                string author = reader["author"].ToString();
                                string image = reader["image"].ToString();

                                BookDTO book = new BookDTO(id, title, author, image);
                                books.Add(book);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching books from the database: " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }

            return books;
        } 
    }
}