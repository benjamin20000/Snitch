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
}

public enum reportType
{
    individual,
    gang
}