using Snitch;
using Bogus;

namespace Snitch;
class Menu
{
      private DbServices dbServices = new DbServices();
      Crud crud = new Crud();
      private Faker faker = new Faker();
      List<string> codeNames = new List<string>();
      
      private string GenerateCodeName()
      {
          // // Generate a random animal and color combination
          string[] animals = new[] { "Tiger", "Lion", "Eagle", "Wolf", "Fox", "Bear", "Hawk", "Dragon", "Falcon", "Cobra" };
          string animal = faker.Random.ArrayElement(animals);
          string color = faker.Commerce.Color();
          string numOfPersons = dbServices.counColumns("persons").ToString();
          Console.WriteLine(numOfPersons);
          return $"{color}{animal}{numOfPersons}";
      }

    
  
      private void creatTables()
      {
        dbServices.CreatePersonsTable();
        dbServices.CreateReportsTable();
      }

      private void snitchErea(long snitch_id)
      {
          Report new_report = Report.createReport(snitch_id);
          new_report.insertReportToTable();


      }
      

      private void signUp()
      {
          Console.WriteLine("enter your first name:");
          string Firstname = Console.ReadLine();
          
          Console.WriteLine("enter your last name:");
          string Lastname = Console.ReadLine();
          
          Console.WriteLine("enter your age:");
          int Age = int.Parse(Console.ReadLine());
          
          Console.WriteLine("enter a new password:");
          string password = Console.ReadLine();
          
          string codeName = GenerateCodeName();


          Person newPerson = new Person();
          newPerson.firstName = Firstname;
          newPerson.LastName = Lastname;
          newPerson.codeName = codeName;
          newPerson.Age = Age;
          newPerson.Password = password;

          long id = newPerson.insertPersonToTable();
          
          Console.WriteLine("welcome to the system: ");
          Console.WriteLine($"your code name is {codeName}");
          Console.WriteLine($"your code password is {password}");

          snitchErea(id);

      }

      private void logIn()
      {
          Console.WriteLine("enter code name:");
          string code_name = Console.ReadLine();
          
          Console.WriteLine("enter password:");
          string password = Console.ReadLine();
          
          var person = dbServices.getOneRowByUniqueVal("persons","codeName",code_name);
          if (person == null || person.Count == 0)
          {
              Console.WriteLine("code name not found");
              return;
          }

          if (person["password"].ToString() != password)
          {
              Console.WriteLine("incorrect password");
              return;
          }
          Console.WriteLine($"hey { code_name } welcome back"); 
          snitchErea(Convert.ToInt64(person["id"]));
      }
    
    private void play_menu()
    {
        Console.WriteLine("Welcome to Snitch!");
        Console.WriteLine("1 for sign up");
        Console.WriteLine("2 for log in");
        
        int choice = int.Parse(Console.ReadLine());

        if (choice == 1)
        {
            signUp();
        }
        else if (choice == 2)
        {
            logIn();
        }
        else
        {
            Console.WriteLine("Invalid choice.");
            return;
        }
    }
    
    static void Main(string[] args)
    {
        Menu menu = new Menu();
        // menu.creatTables();
        menu.play_menu();
        
    }
}