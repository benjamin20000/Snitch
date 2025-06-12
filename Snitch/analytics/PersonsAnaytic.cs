namespace Snitch;

public class PersonsAnaytic
{
    public static void printReporters()
    {
        string query = "SELECT reports.snitch_id, persons.codeName, COUNT(*) AS reports_made FROM reports join persons on reports.target_code_name =  persons.codeName GROUP BY snitch_id order by reports_made desc;";
        var res = Crud.generalGetFuc(query);
        foreach (var r in res)
        {
            foreach (var r2 in r)
            {
                Console.Write(r2);
            }
            Console.WriteLine();
        }
    }
    
    public static void printReporteds()
    {
        string query = "SELECT target_code_name, COUNT(*) AS reported_count FROM reports  GROUP BY target_code_name order by reported_count desc;";
        var res = Crud.generalGetFuc(query);
        foreach (var r in res)
        {
            foreach (var r2 in r)
            {
                Console.Write(r2);
            }
            Console.WriteLine();
        }
    }
    
}