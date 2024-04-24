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
        DatabaseConnection dbConnection = new DatabaseConnection();
        dbConnection.OpenConnection();

        try
        {
            using (SqlConnection connection = dbConnection.GetSqlConnection())
            {
                string insertQuery = "INSERT INTO dbo.category (name) VALUES (@name);";

                using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@name", categoryDTO.Name);

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

            CategoryDTO categoryDTO = new CategoryDTO();
        List<CategoryDTO> categories = new List<CategoryDTO>();
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.OpenConnection();

            try
            {
                using (SqlConnection connection = dbConnection.GetSqlConnection())
                {
                    string selectQuery = "SELECT id, name FROM dbo.category";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id"));
                                string name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("Name"));
                                
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
                dbConnection.CloseConnection();
            }

            return categories;
        }

        public void AddCategory(Category newCategory)
        {
            SendCategory(newCategory);
        }


}
