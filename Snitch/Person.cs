using System.Security.AccessControl;

namespace Snitch;

public class Person
{
    public long id { get; set; }
    public string firstName { get; set; }
    public string LastName { get; set; }
    public string codeName { get; set; }
    public int Age { get; set; }
    public string Password { get; set; }
    private Crud crud = new Crud();
    private DbServices db_services = new DbServices();

    // public Person(string firstName, string lastName, string codeName, string email, int age)
    // {
    //     this.firstName = firstName;
    //     this.LastName = lastName;
    //     this.codeName = codeName;
    //     this.Age = age;
    //     this.Password = email;
    //     insertPersonToTable();
    // }

    public long insertPersonToTable()
    {
        Dictionary<string,object> data = new Dictionary<string, object>(){
            {"firstName", firstName},
            {"lastName", LastName},
            {"codeName", codeName},
            {"age", Age},
            {"password", Password},
        };
        return crud.InsertRow("persons",data); 
    }
}