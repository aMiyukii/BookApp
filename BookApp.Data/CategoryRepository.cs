    using System.Data.SqlClient;
    using BookApp.Core.Models;
    using System.Xml.Linq;
    using DAL;
    using BookApp.Core.DTO;
    using BookApp.Core.Models;

    namespace BookApp.Data;

    public class CategoryRepository
    {
    public void SendCategory(Category category)
    {
        CategoryDTO categoryDTO = new CategoryDTO();
        categoryDTO.Name = category.Name;
        categoryDTO.IsStandard = false;

        DatabaseConnection dbConnection = new DatabaseConnection();
        dbConnection.OpenConnection();

        try
        {
            using (SqlConnection connection = dbConnection.GetSqlConnection())
            {
                string insertQuery = "INSERT INTO dbo.category (name, isStandard) VALUES (@name, @isStandard);";

                using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@name", categoryDTO.Name);
                    cmd.Parameters.AddWithValue("@isStandard", categoryDTO.IsStandard);

                    cmd.ExecuteNonQuery();
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
            dbConnection.CloseConnection();
        }
    }


    public List<CategoryDTO> GetAllCategory()
    {
        List<CategoryDTO> categories = new List<CategoryDTO>();
        DatabaseConnection dbConnection = new DatabaseConnection();
        dbConnection.OpenConnection();

        try
        {
            using (SqlConnection connection = dbConnection.GetSqlConnection())
            {
                string selectQuery = "SELECT id, name, isStandard FROM dbo.category";

                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32(reader.GetOrdinal("id"));
                            string name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name"));
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
            dbConnection.CloseConnection();
        }

        return categories;
    }


    public void AddCategory(Category newCategory)
            {
                SendCategory(newCategory);
            }

            public void UpdateCategory(Category category)
            {
                DatabaseConnection dbConnection = new DatabaseConnection();
                dbConnection.OpenConnection();

                try
                {
                    using (SqlConnection connection = dbConnection.GetSqlConnection())
                    {
                        string updateQuery = "UPDATE dbo.category SET name = @name WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@name", category.Name);
                            cmd.Parameters.AddWithValue("@id", category.Id);

                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Category name updated successfully.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    dbConnection.CloseConnection();
                }
            }

            public void DeleteCategory(int id)
            {
                DatabaseConnection dbConnection = new DatabaseConnection();
                dbConnection.OpenConnection();

                try
                {
                    using (SqlConnection connection = dbConnection.GetSqlConnection())
                    {
                        string deleteQuery = "DELETE FROM dbo.category WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", id);

                            cmd.ExecuteNonQuery();
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
                    dbConnection.CloseConnection();
                }
            }

    }
