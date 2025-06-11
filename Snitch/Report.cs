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
    public int? target_id { get; set; }      // Nullable for gang reports
    public int? gang_id { get; set; }        // Nullable for individual reports
    private Crud crud = new Crud();

    public void insertReportToTable()
    {
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            {"snitch_id", snitch_id},
            {"report_text", report_text},
            {"location", location},
            {"target_type", target_type},
            {"target_id", target_id.HasValue ? target_id.Value : DBNull.Value},
            {"gang_id", gang_id.HasValue ? gang_id.Value : DBNull.Value}
        };
        crud.InsertRow("reports", data);
    }
    
    public static Report createReport(long snitch_id)
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
            report.target_type = "Individual";
            Console.WriteLine("enter target id");
            int id = int.Parse(Console.ReadLine());
            report.target_id = id;
            report.gang_id = null;
        }
        else if(choice == 2)
        {
            report.target_type = "Gang";
            Console.WriteLine("enter gang id");
            int id = int.Parse(Console.ReadLine());
            report.gang_id = id;
            report.target_id = null;
        }
        
        Console.WriteLine("enter report text");
        string text = Console.ReadLine();
        report.report_text = text;
        
        report.report_time = DateTime.Now;
        return report;
    }
}