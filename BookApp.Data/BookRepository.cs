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
using BookApp.Core.Interfaces;
using BookApp.Core.Services;

namespace BookApp.Data
{
    public class BookRepository : IBookRepository
    {
        public async Task<List<BookDTO>> GetAllAsync()
        {
            List<BookDTO> books = new List<BookDTO>();
            DatabaseConnection dbConnection = new DatabaseConnection();
            await dbConnection.OpenConnectionAsync();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string selectQuery = "SELECT id, title, author, imageUrl FROM dbo.book";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int id = Convert.ToInt32(reader["id"]);
                                string title = reader["title"].ToString();
                                string author = reader["author"].ToString();
                                string imageUrl = reader["imageUrl"].ToString();

                                BookDTO book = new BookDTO(id, title, author, imageUrl);
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
                await dbConnection.CloseConnectionAsync();
            }

            return books;
        }

        public async Task<string> GetBookTitleByIdAsync(int bookId)
        {
            string title = null;
            DatabaseConnection dbConnection = new DatabaseConnection();
            await dbConnection.OpenConnectionAsync();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string selectQuery = "SELECT title FROM dbo.book WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", bookId);

                        object result = await cmd.ExecuteScalarAsync();
                        if (result != null)
                        {
                            title = result.ToString();
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
                await dbConnection.CloseConnectionAsync();
            }

            return title;
        }

        public async Task AddBookToUserCollectionAsync(int bookId, int categoryId1, int? categoryId2 = null)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            await dbConnection.OpenConnectionAsync();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string checkBookQuery = "SELECT COUNT(*) FROM dbo.book WHERE id = @bookId";

                    using (SqlCommand checkCmd = new SqlCommand(checkBookQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@bookId", bookId);
                        int count = (int)await checkCmd.ExecuteScalarAsync();

                        if (count > 0)
                        {
                            string insertUserBookQuery =
                                "INSERT INTO user_book (book_id) OUTPUT INSERTED.id VALUES (@bookId)";

                            using (SqlCommand insertUserBookCmd = new SqlCommand(insertUserBookQuery, connection))
                            {
                                insertUserBookCmd.Parameters.AddWithValue("@bookId", bookId);
                                int userBookId = (int)await insertUserBookCmd.ExecuteScalarAsync();

                                string insertUserBookCategoryQuery =
                                    "INSERT INTO user_book_category (category_id, category_id_2, user_book_id) VALUES (@categoryId1, @categoryId2, @userBookId)";

                                using (SqlCommand insertUserBookCategoryCmd =
                                       new SqlCommand(insertUserBookCategoryQuery, connection))
                                {
                                    insertUserBookCategoryCmd.Parameters.AddWithValue("@categoryId1", categoryId1);
                                    insertUserBookCategoryCmd.Parameters.AddWithValue("@categoryId2",
                                        (object)categoryId2 ?? DBNull.Value);
                                    insertUserBookCategoryCmd.Parameters.AddWithValue("@userBookId", userBookId);
                                    await insertUserBookCategoryCmd.ExecuteNonQueryAsync();
                                    Console.WriteLine("Book added to user collection successfully with categories.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: Book with ID " + bookId + " does not exist.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding book to user collection: " + ex.Message);
            }
            finally
            {
                await dbConnection.CloseConnectionAsync();
            }
        }

        public async Task<List<BookDTO>> GetBooksInLibraryAsync()
        {
            List<BookDTO> books = new List<BookDTO>();
            DatabaseConnection dbConnection = new DatabaseConnection();
            await dbConnection.OpenConnectionAsync();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string selectQuery =
                        "SELECT title, author, imageUrl FROM user_book JOIN book ON user_book.book_id = book.id";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string title = reader["title"].ToString();
                                string author = reader["author"].ToString();
                                string imageUrl = reader["imageUrl"].ToString();

                                BookDTO book = new BookDTO { Title = title, Author = author, ImageUrl = imageUrl };
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
                await dbConnection.CloseConnectionAsync();
            }

            return books;
        }

        public async Task<BookDTO> GetBookByTitleAsync(string title)
        {
            BookDTO book = null;
            DatabaseConnection dbConnection = new DatabaseConnection();
            await dbConnection.OpenConnectionAsync();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string selectQuery =
                        "SELECT id, title, author, imageUrl, serie, genre FROM dbo.book WHERE title = @title";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@title", title);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                int id = Convert.ToInt32(reader["id"]);
                                string author = reader["author"].ToString();
                                string imageUrl = reader["imageUrl"].ToString();
                                string serie = reader["serie"].ToString();
                                string genre = reader["genre"].ToString();

                                book = new BookDTO(id, title, author, imageUrl, serie, genre);
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
                await dbConnection.CloseConnectionAsync();
            }

            return book;
        }

        public async Task DeleteBookByTitleAsync(string title)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            await dbConnection.OpenConnectionAsync();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string deleteQuery = "DELETE FROM dbo.book WHERE title = @title";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@title", title);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting book from the database: " + ex.Message);
            }
            finally
            {
                await dbConnection.CloseConnectionAsync();
            }
        }

        public async Task DeleteUserBookByBookIdAsync(int bookId)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            await dbConnection.OpenConnectionAsync();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string deleteQuery = "DELETE FROM user_book WHERE book_id = @bookId";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@bookId", bookId);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting user book from the database: " + ex.Message);
            }
            finally
            {
                await dbConnection.CloseConnectionAsync();
            }
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesByBookIdAsync(int bookId)
        {
            List<CategoryDTO> categories = new List<CategoryDTO>();
            DatabaseConnection dbConnection = new DatabaseConnection();
            await dbConnection.OpenConnectionAsync();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string selectQuery = "SELECT c.id, c.name FROM category c " +
                                         "INNER JOIN user_book_category ubc ON c.id = ubc.category_id OR c.id = ubc.category_id_2 " +
                                         "INNER JOIN user_book ub ON ubc.user_book_id = ub.id " +
                                         "WHERE ub.book_id = @bookId";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@bookId", bookId);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int id = Convert.ToInt32(reader["id"]);
                                string name = reader["name"].ToString();

                                CategoryDTO category = new CategoryDTO(id, name);
                                categories.Add(category);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching categories from the database: " + ex.Message);
            }
            finally
            {
                await dbConnection.CloseConnectionAsync();
            }

            return categories;
        }

        public async Task SaveCategoryAsync(int userBookId, int categoryId1, int categoryId2)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            await dbConnection.OpenConnectionAsync();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string updateQuery =
                        "UPDATE user_book_category SET category_id = @categoryId1, category_id_2 = @categoryId2 WHERE user_book_id = @userBookId";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@categoryId1", categoryId1);
                        cmd.Parameters.AddWithValue("@categoryId2", categoryId2);
                        cmd.Parameters.AddWithValue("@userBookId", userBookId);

                        await cmd.ExecuteNonQueryAsync();
                        Console.WriteLine("Categories saved successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving categories: " + ex.Message);
            }
            finally
            {
                await dbConnection.CloseConnectionAsync();
            }
        }

        public async Task<bool> IsBookInUserCollectionAsync(int bookId)
        {
            bool isInCollection = false;
            DatabaseConnection dbConnection = new DatabaseConnection();
            await dbConnection.OpenConnectionAsync();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string checkQuery = "SELECT COUNT(*) FROM user_book WHERE book_id = @bookId";

                    using (SqlCommand cmd = new SqlCommand(checkQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@bookId", bookId);
                        int count = (int)await cmd.ExecuteScalarAsync();
                        isInCollection = count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error checking if book is in user collection: " + ex.Message);
            }
            finally
            {
                await dbConnection.CloseConnectionAsync();
            }

            return isInCollection;
        }
        
        public async Task<List<BookDTO>> GetBooksByUserIdAsync(int userId)
        {
            List<BookDTO> books = new List<BookDTO>();
            DatabaseConnection dbConnection = new DatabaseConnection();
            await dbConnection.OpenConnectionAsync();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string selectQuery =
                        "SELECT b.id, b.title, b.author, b.imageUrl FROM user_book ub " +
                        "JOIN book b ON ub.book_id = b.id " +
                        "WHERE ub.user_id = @userId";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int id = Convert.ToInt32(reader["id"]);
                                string title = reader["title"].ToString();
                                string author = reader["author"].ToString();
                                string imageUrl = reader["imageUrl"].ToString();

                                BookDTO book = new BookDTO(id, title, author, imageUrl);
                                books.Add(book);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching books by user ID from the database: " + ex.Message);
            }
            finally
            {
                await dbConnection.CloseConnectionAsync();
            }

            return books;
        }
    }
}