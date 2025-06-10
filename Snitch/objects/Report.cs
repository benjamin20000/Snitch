using Snitch.db;


namespace Snitch.objects;

public class Report
{
    public int reportID {get; set;}
    public string reportText {get; set;}
    public string location {get; set;}
    public int reportTime {get; set;}
    public int snitchID {get; set;}
    public int targetID {get; set;}
    public int gangID {get; set;}
    public reportType reportType {get; set;}

    public void AddReport()
    {
        Crud crud = new Crud();
        
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("reportID", reportID);
        data.Add("reportText", reportText);
        data.Add("location", location);
        data.Add("reportTime", reportTime);
        data.Add("snitchID", snitchID);
        data.Add("targetID", targetID);
        data.Add("gangID", gangID);
        data.Add("reportType", reportType);
        crud.InsertRow("report", data);
    }
}

public enum reportType
{
    individual,
    gang
}