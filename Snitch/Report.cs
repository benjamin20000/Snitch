using Bogus.Extensions.Italy;

namespace Snitch;

enum TargetType
{
    Individual,
    Gang
}

public class Report
{
    public int report_id { get; set; }
    public long snitch_id { get; set; }
    public string report_text { get; set; }
    public string location { get; set; }
    public DateTime report_time { get; set; }
    public string target_type { get; set; }  // individual or gang
    public string target_code_name { get; set; }      // Nullable for gang reports
    public int? gang_id { get; set; }        // Nullable for individual reports
    private DbServices db_services = new DbServices();

    public void insertReportToTable()
    {
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            {"snitch_id", snitch_id},
            {"report_text", report_text},
            {"location", location},
            {"target_type", target_type},
            {"target_code_name", target_code_name},
            {"gang_id", gang_id.HasValue ? gang_id.Value : DBNull.Value}
        };
        Crud.InsertRow("reports", data);
    }
    
    public static Report createReport(long snitch_id, DbServices db_services)
    {
        Console.WriteLine("lets create a new report");
        Console.WriteLine("what type of target is this report about?");
        Console.WriteLine("1. Individual");
        Console.WriteLine("2. Gang");
        Console.WriteLine("3. Exit");
        int choice = int.Parse(Console.ReadLine());
        if(choice >= 3)
        {
            Console.WriteLine("Exiting report creation...");
            Environment.Exit(0);
            return null;
        }
        Report report = new Report();
        report.snitch_id = snitch_id;
        
        if (choice == 1)
        {
            Console.WriteLine("does the target known to us?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            int known = int.Parse(Console.ReadLine());
            string target_code_name = "";
            
            if(known == 1){ //target already exists
                Console.WriteLine("enter target code name");
                target_code_name = Console.ReadLine();
                if (!db_services.checkPersonExits(target_code_name))
                {
                    Console.WriteLine("no target code exists");
                    Environment.Exit(0);
                }
            }
            if (known == 2)
            {
                Console.WriteLine("lets create a new target");
                Person newTarget = Person.createPerson(db_services);
                target_code_name = newTarget.codeName;
                newTarget.insertPersonToTable();
            }
            
            report.target_type = "Individual";
            report.target_code_name = target_code_name;
            report.gang_id = null;
        }
        else if(choice == 2)
        {
            report.target_type = "Gang";
            Console.WriteLine("enter gang id");
            int id = int.Parse(Console.ReadLine());
            report.gang_id = id;
            report.target_code_name = null;
        }
        
        Console.WriteLine("enter report text");
        string text = Console.ReadLine();
        report.report_text = text;
        
        report.report_time = DateTime.Now;
        return report;
    }
}