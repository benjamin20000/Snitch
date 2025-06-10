namespace Snitch.objects;
using Snitch.db;

public class Person
{
    public int id { get; set; }
    public string firstName { get; set; } 
    public string lastName { get; set; }
    public string codeName { get; set; }
    public string password { get; set; }
    public string address { get; set; }
    public int phoneNumber { get; set; }
    public string email { get; set; }
    public string trustLevel { get; set; }
    public string dangerLevel { get; set; }



    public void AddPerson()
    {
        Crud crud = new Crud();
        Dictionary<string, object> data = new Dictionary<string, object>();
        data["id"] = id;
        data["firstName"] = firstName;
        data["lastName"] = lastName;
        data["codeName"] = codeName;
        data["password"] = password;
        data["address"] = address;
        data["phoneNumber"] = phoneNumber;
        data["email"] = email;
        data["trustLevel"] = trustLevel;
        data["dangerLevel"] = dangerLevel;
        crud.InsertRow("person", data);
    }
}