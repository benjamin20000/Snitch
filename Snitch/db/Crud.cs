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

    public void DropTable(string tableName)
    {
        string query = $"DROP TABLE IF EXISTS `{tableName}`";
        try
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Table `{tableName}` deleted successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error creating table: " + ex.Message);
        }
    }

     
    
    
    public void InsertRow(string tableName, Dictionary<string, object> data)
    {
        if (data == null || data.Count == 0)
        {
            throw new ArgumentException("row cant by null or empty");
        }
        
        string columnNames = string.Join(", ", data.Keys.Select(k => $"`{k}`"));
        string parameterNames = string.Join(", ", data.Keys.Select(k => $"@{k}"));
        string query = $"INSERT INTO `{tableName}` ({columnNames}) VALUES ({parameterNames})";
        
        using (var connection = new MySqlConnection(connectionString))
        {
            using (var command = new MySqlCommand(query, connection))
            {
                foreach (var item in data)
                {
                    command.Parameters.AddWithValue($"@{item.Key}", item.Value ?? DBNull.Value);
                }

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery(); 
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Database error occurred: {ex.Message}");
                    throw;
                }
            }
        }
    }
//    public List<Dictionary<string, object>> GetTable(
//     string tableName,
//     List<string>? columns = null,
//     Dictionary<string, Tuple<object, ComparisonOperator>>? conditions = null)
//     {
//     var results = new List<Dictionary<string, object>>();
//
//     // Validate table and column names to avoid SQL injection via identifiers
//     string SafeIdentifier(string name)
//     {
//         // Basic identifier protection: allow only alphanumerics and underscore
//         if (!System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z0-9_]+$"))
//             throw new ArgumentException("Invalid identifier: " + name);
//         return $"`{name}`";
//     }
//
//     // --- 1. SELECT clause ---
//     string selectColumns = (columns == null || columns.Count == 0 || columns.Contains("*"))
//         ? "*"
//         : string.Join(", ", columns.Select(SafeIdentifier));
//
//     string query = $"SELECT {selectColumns} FROM {SafeIdentifier(tableName)}";
//
//     // --- 2. WHERE clause ---
//     var whereClauses = new List<string>();
//     var parameters = new List<MySqlParameter>();
//
//     if (conditions != null)
//     {
//         int index = 0;
//         foreach (var kvp in conditions)
//         {
//             string col = kvp.Key;
//             object value = kvp.Value.Item1;
//             ComparisonOperator op = kvp.Value.Item2;
//
//             string paramName = $"@p{index++}";
//             string sqlOperator = op switch
//             {
//                 ComparisonOperator.Equals => "=",
//                 ComparisonOperator.NotEquals => "!=",
//                 ComparisonOperator.GreaterThan => ">",
//                 ComparisonOperator.LessThan => "<",
//                 _ => throw new ArgumentOutOfRangeException()
//             };
//
//             whereClauses.Add($"{SafeIdentifier(col)} {sqlOperator} {paramName}");
//             parameters.Add(new MySqlParameter(paramName, value));
//         }
//
//         if (whereClauses.Count > 0)
//         {
//             query += " WHERE " + string.Join(" AND ", whereClauses);
//         }
//     }
//
//     query += ";";
//
//     // --- 3. Execute command ---
//     try
//     {
//         using var conn = new MySqlConnection(connectionString);
//         using var cmd = new MySqlCommand(query, conn);
//         cmd.Parameters.AddRange(parameters.ToArray());
//
//         conn.Open();
//         using var reader = cmd.ExecuteReader();
//         while (reader.Read())
//         {
//             var row = new Dictionary<string, object>();
//             for (int i = 0; i < reader.FieldCount; i++)
//             {
//                 row[reader.GetName(i)] = reader.GetValue(i);
//             }
//             results.Add(row);
//         }
//     }
//     catch (Exception ex)
//     {
//         Console.WriteLine($"Error querying `{tableName}`: {ex.Message}");
//     }
//
//     return results;
// }
//
//

}