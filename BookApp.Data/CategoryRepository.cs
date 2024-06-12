using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BookApp.Core.DTO;
using BookApp.Core.Interfaces;
using BookApp.Core.Services;
using DAL;

namespace BookApp.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            List<CategoryDTO> categories = new List<CategoryDTO>();
            DatabaseConnection dbConnection = new DatabaseConnection();
            await dbConnection.OpenConnectionAsync();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string selectQuery = "SELECT id, name, isStandard FROM dbo.category";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("id"));
                                string name = reader.GetString(reader.GetOrdinal("name"));
                                bool isStandard = reader.GetBoolean(reader.GetOrdinal("isStandard"));

                                CategoryDTO category = new CategoryDTO(id, name, isStandard);
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

        public async Task AddCategoryAsync(CategoryDTO category)
        {
            await SendCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            await dbConnection.OpenConnectionAsync();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string deleteQuery = "DELETE FROM dbo.category WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        await cmd.ExecuteNonQueryAsync();
                        Console.WriteLine("Category deleted successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                await dbConnection.CloseConnectionAsync();
            }
        }

        public async Task<List<CategoryDTO>> GetAllCategoryAsync()
        {
            List<CategoryDTO> categories = new List<CategoryDTO>();
            DatabaseConnection dbConnection = new DatabaseConnection();
            await dbConnection.OpenConnectionAsync();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string selectQuery = "SELECT id, name, isStandard FROM dbo.category";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int id = reader.IsDBNull(reader.GetOrdinal("id"))
                                    ? 0
                                    : reader.GetInt32(reader.GetOrdinal("id"));
                                string name = reader.IsDBNull(reader.GetOrdinal("name"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("name"));
                                bool isStandard = reader.GetBoolean(reader.GetOrdinal("isStandard"));

                                CategoryDTO category = new CategoryDTO(id, name, isStandard);
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

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            CategoryDTO category = null;
            DatabaseConnection dbConnection = new DatabaseConnection();
            await dbConnection.OpenConnectionAsync();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string selectQuery = "SELECT id, name, isStandard FROM dbo.category WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                string name = reader.IsDBNull(reader.GetOrdinal("name"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("name"));
                                bool isStandard = reader.GetBoolean(reader.GetOrdinal("isStandard"));

                                category = new CategoryDTO(id, name, isStandard);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching category from the database: " + ex.Message);
            }
            finally
            {
                await dbConnection.CloseConnectionAsync();
            }

            return category;
        }

        public async Task UpdateCategoryAsync(CategoryDTO category)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            await dbConnection.OpenConnectionAsync();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string updateQuery =
                        "UPDATE dbo.category SET name = @name, isStandard = @isStandard WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", category.Name);
                        cmd.Parameters.AddWithValue("@isStandard", category.IsStandard);
                        cmd.Parameters.AddWithValue("@id", category.Id);

                        await cmd.ExecuteNonQueryAsync();
                        Console.WriteLine("Category updated successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                await dbConnection.CloseConnectionAsync();
            }
        }

        private async Task SendCategoryAsync(CategoryDTO category)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            await dbConnection.OpenConnectionAsync();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string insertQuery = "INSERT INTO dbo.category (name, isStandard) VALUES (@name, @isStandard);";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", category.Name);
                        cmd.Parameters.AddWithValue("@isStandard", category.IsStandard);

                        await cmd.ExecuteNonQueryAsync();
                        Console.WriteLine("Category added successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                await dbConnection.CloseConnectionAsync();
            }
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
        
        public async Task<CategoryDTO> GetCategoryByNameAsync(string name)
        {
            CategoryDTO category = null;
            DatabaseConnection dbConnection = new DatabaseConnection();
            await dbConnection.OpenConnectionAsync();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string selectQuery = "SELECT id, name, isStandard FROM dbo.category WHERE name = @name";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", name);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                int id = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32(reader.GetOrdinal("id"));
                                bool isStandard = reader.GetBoolean(reader.GetOrdinal("isStandard"));

                                category = new CategoryDTO(id, name, isStandard);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching category by name from the database: " + ex.Message);
            }
            finally
            {
                await dbConnection.CloseConnectionAsync();
            }

            return category;
        }

    }
}