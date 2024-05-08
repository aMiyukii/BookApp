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
                    string selectQuery = "SELECT id, title, author, image_url FROM dbo.book";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["id"]);
                                string title = reader["title"].ToString();
                                string author = reader["author"].ToString();
                                string imageurl = reader["image_url"].ToString();

                                BookDTO book = new BookDTO(id, title, author, imageurl);
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
        
        public string GetBookTitleById(int bookId)
        {
            string title = null;
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.OpenConnection();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string selectQuery = "SELECT title FROM dbo.book WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", bookId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                title = reader["title"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching book title from the database: " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }

            return title;
        }
        
        public void AddBookToUserCollection(int bookId)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.OpenConnection();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string insertQuery = "INSERT INTO user_book (book_id) VALUES (@bookId)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@bookId", bookId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding book to user collection: " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }
        
        public List<Book> GetBooksInLibrary()
        {
            List<Book> books = new List<Book>();
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.OpenConnection();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string selectQuery = "SELECT title, author FROM user_book JOIN book ON user_book.book_id = book.id";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string title = reader["title"].ToString();
                                string author = reader["author"].ToString();

                                Book book = new Book { Title = title, Author = author };
                                books.Add(book);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching books from the library: " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }

            return books;
        }
        public BookDTO GetBookByTitle(string title)
        {
            BookDTO book = null;
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.OpenConnection();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string selectQuery = "SELECT id, title, author, image, serie, genre FROM dbo.book WHERE title = @title";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@title", title);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["id"]);
                                string author = reader["author"].ToString();
                                string image = reader["image"].ToString();
                                string serie = reader["serie"].ToString();
                                string genre = reader["genre"].ToString();

                                book = new BookDTO(id, title, author, image, serie, genre);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching book from the database by title: " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }

            return book;
        }
        
        public void DeleteBookByTitle(string title)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.OpenConnection();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string deleteQuery = "DELETE FROM dbo.book WHERE title = @title";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting book from the database: " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }
        
        public void DeleteUserBookByBookId(int bookId)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.OpenConnection();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string deleteQuery = "DELETE FROM user_book WHERE book_id = @bookId";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@bookId", bookId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting user book from the database: " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }

    }
}