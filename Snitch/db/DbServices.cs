namespace Snitch;

public class DbServices
{
    public void CreatePersonsTable()
    {
        string schema = @"
            id INT PRIMARY KEY AUTO_INCREMENT,
            firstName VARCHAR(255),
            lastName VARCHAR(255),
            codeName VARCHAR(255) UNIQUE,
            age INT,
            password VARCHAR(255),
            address VARCHAR(255),
            phoneNumber VARCHAR(255),
            email VARCHAR(255),
            trust_level INT DEFAULT 0,
            danger_level INT DEFAULT 0
        ";
        Crud.CreateTable("persons", schema);
    }

    public void CreateReportsTable()
    {
        string schema = @"
            report_id INT PRIMARY KEY AUTO_INCREMENT,
            snitch_id INT NOT NULL,
            report_text TEXT NOT NULL,
            location VARCHAR(255),
            report_time DATETIME DEFAULT CURRENT_TIMESTAMP,
            target_type ENUM('individual', 'gang') NOT NULL,
            target_code_name VARCHAR(255) NULL,
            gang_id INT NULL,
            FOREIGN KEY (snitch_id) REFERENCES persons(id),
            FOREIGN KEY (target_code_name) REFERENCES persons(codeName)
        ";
        Crud.CreateTable("reports", schema);
    }
    
    

    public int counRows(string table_name)
    {
        List<string> column= new List<string>() { "id" };
        var dic = Crud.GetTable(table_name, column);
        return dic.Count;
    }

    public Dictionary<string, object> getOneRowByUniqueVal(string table_name, string column, object value)
    {
        Dictionary<string, object> cond = new Dictionary<string, object>()
        {
            {column,value}
        };
        var table = Crud.GetTable(table_name, null, cond);
        return table[0];
    }

    public void printRowByUniqueVal(string table_name, string column, object value)
    {
        Dictionary<string, object> row = getOneRowByUniqueVal(table_name, column, value);
        foreach (KeyValuePair<string, object> kvp in row)
        {
            Console.WriteLine($"Key = {kvp.Key}, Value = {kvp.Value}");
        }
    }

    // public bool validitPersonPassword(string codeName, string password)
    // {
    //     Dictionary<string, object> details = new Dictionary<string, object>()
    //     {
    //         { "codeName", codeName },
    //         { "password", password }
    //     };
    //     var res = Crud.GetTable("persons",null ,details);
    //     if (res.Count > 0)
    //     {
    //         return true;
    //     }
    //     return false;
    // }

    public bool checkPersonExits(string codeName)
    {
        Dictionary<string, object> details = new Dictionary<string, object>()
        {
            { "codeName", codeName },
        };
        var res = Crud.GetTable("persons",null ,details);
        if (res.Count > 0)
        {
            return true;
        }
        return false;
    }
    // public Dictionary<string, object> getPersonFromTableByCodeName(string codeName)
    // {
    //     return getOneRowByUniqueVal("persons", "codeName", codeName);
    // }
    //
    // public long getPersonIdByCodeName(string codeName)
    // {
    //     var person = getPersonFromTableByCodeName(codeName);
    //     if (person.Count > 0)
    //     {
    //         return Convert.ToInt64(person["id"]);
    //     }
    //     return -1; // return -1 if no person with this codeName
    // }

}