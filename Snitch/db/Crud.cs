using MySql.Data.MySqlClient;

namespace Snitch.db;



public enum ComparisonOperator
{
    Equals,         // =
    NotEquals,      // != or <>
    GreaterThan,    // >
    LessThan       // <
}

public class Crud
{
    private string connectionString;

    public Crud()
    {
        connectionString =
            "server=localhost;" +
            "user=root;" +
            "database=Snitch;" +
            "port=3306;";
    }
    
    public void CreateTable(string tableName, Dictionary<string, string> schema)
    {
        using (var conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();

                var columnDefinitions = new List<string>();
                foreach (var column in schema)
                {
                    columnDefinitions.Add($"`{column.Key}` {column.Value}");
                }
                string query = $"CREATE TABLE IF NOT EXISTS `{tableName}` ({string.Join(", ", columnDefinitions)})";

                var cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                Console.WriteLine($"Table `{tableName}` created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating table: " + ex.Message);
            }
        }
    }

    public List<Dictionary<string, object>> GetTable(
        string tableName,
        string columns = "*",
        string conditions = "1")
    {
        var results = new List<Dictionary<string, object>>();

        // Validate identifiers: basic pattern for safety
        bool IsSafeIdentifier(string value) =>
            System.Text.RegularExpressions.Regex.IsMatch(value, @"^[a-zA-Z0-9_,\s\*]+$");

        if (!IsSafeIdentifier(tableName) || !IsSafeIdentifier(columns) || !IsSafeIdentifier(conditions))
        {
            throw new ArgumentException("Unsafe input detected.");
        }

        // Build SQL (dangerous if not validated!)
        string query = $"SELECT {columns} FROM `{tableName}` WHERE {conditions}";

        try
        {
            using var conn = new MySqlConnection(connectionString);
            using var cmd = new MySqlCommand(query, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.GetValue(i);
                }
                results.Add(row);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting data from `{tableName}`: {ex.Message}");
        }

        return results;
    }


}