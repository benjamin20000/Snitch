using System.Security.AccessControl;
using Bogus;

namespace Snitch;

public class Person
{
    public long id { get; set; }
    private string firstName { get; set; }
    private string LastName { get; set; }
    public string codeName { get; set; }
    private int Age { get; set; }
    public string Password { get; set; }
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
        return Crud.InsertRow("persons",data); 
    }
    private static string GenerateCodeName(DbServices dbServices)
    {
        Faker faker = new Faker();
        string[] animals = new[] { "Tiger", "Lion", "Eagle", "Wolf", "Fox", "Bear", "Hawk", "Dragon", "Falcon", "Cobra" };
        string animal = faker.Random.ArrayElement(animals);
        string color = faker.Commerce.Color();
        string numOfPersons = dbServices.counRows("persons").ToString();
        string newCodeName = $"{color}{animal}{numOfPersons}";
        Console.WriteLine($"new code name Generated {newCodeName}");
        return newCodeName;
    }

    public static Person createPerson(DbServices dbServices)
    {
        Console.WriteLine("enter a new name:");
        string name = Console.ReadLine();
        Console.WriteLine("enter a last name:");
        string lastName = Console.ReadLine();
        Console.WriteLine("enter a age:");
        int age = int.Parse(Console.ReadLine());
        string newCodeName = GenerateCodeName(dbServices);
        
        Person person = new Person();
        person.firstName = name;
        person.LastName = lastName;
        person.Age = age;
        person.codeName = newCodeName;
        return person;
    }
}