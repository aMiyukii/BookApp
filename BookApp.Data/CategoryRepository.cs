using System.Data.SqlClient;
using DAL;

namespace BookApp.Data;

public class CategoryRepository
{
    private int Id { get; set; }
    private string Name { get; set; }
    private bool IsStandard { get; set; }

    public CategoryRepository(int id, string name, bool isStandard)
    {
        Id = id;
        Name = name;
        IsStandard = isStandard;
    }
    public CategoryRepository(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static List<CategoryRepository> SendCategoryToDatabase()
    {
        List<CategoryRepository> categories = new List<CategoryRepository>();
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

                            int id = Convert.ToInt32(reader["id"]);
                            string name = reader["name"].ToString();
                            // bool isStandard = reader["isStandard"].ToString();

                            CategoryRepository category = new CategoryRepository(id, name);
                            categories.Add(category);

                            Console.WriteLine($"Category added {categories}");
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
}