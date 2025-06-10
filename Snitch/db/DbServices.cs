namespace Snitch;

public class DbServices
{
    private Crud crud = new Crud();

    public void CreatePersonsTable()
    {
        var schema = new Dictionary<string, string>
        {
            { "id", "INT PRIMARY KEY AUTO_INCREMENT" },
            { "firstName", "VARCHAR(255)" },
            { "lastName", "VARCHAR(255)" },
            { "codeName", "VARCHAR(255)" },
            { "age", "INT" },
            { "password", "VARCHAR(255)" },
            { "address", "VARCHAR(255)" },
            { "phoneNumber", "VARCHAR(255)" },
            { "email", "VARCHAR(255)" },
            { "trust_level", "INT DEFAULT 0" },  
            { "danger_level", "INT DEFAULT 0" } 
        };
        crud.CreateTable("persons", schema);
    }

    public void CreateReportsTable()
    {
        var schema = new Dictionary<string, string>
        {
            { "report_id", "INT PRIMARY KEY AUTO_INCREMENT" },
            { "snitch_id", "INT NOT NULL" },
            { "report_text", "TEXT NOT NULL" },
            { "location", "VARCHAR(255)" },
            { "report_time", "DATETIME DEFAULT CURRENT_TIMESTAMP" },
            { "target_type", "ENUM('individual', 'gang') NOT NULL" },
            { "target_id", "INT NULL" },
            { "gang_id", "INT NULL" },
            { "FOREIGN KEY (snitch_id)", "REFERENCES persons(id)" },
            { "FOREIGN KEY (target_id)", "REFERENCES persons(id)" }
        };
        crud.CreateTable("reports", schema);
    }

    public int getNumOfPersons()
    {
        List<string> column= new List<string>() { "id" };
        var dic = crud.GetTable("persons", column);
        return dic.Count;
    }

    public bool checkIfPersonExists(string codeName, string password)
    {
        Dictionary<string, object> details = new Dictionary<string, object>()
        {
            { "codeName", codeName },
            { "password", password }
        };
        var res = crud.GetTable("persons",null ,details);
        if (res.Count > 0)
        {
            return true;
        }
        return false;
    }

}