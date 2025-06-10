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
          string numOfPersons = dbServices.getNumOfPersons().ToString();
          Console.WriteLine(numOfPersons);
          return $"{color}{animal}{numOfPersons}";
      }

    
  
      private void creatTables()
      {
        dbServices.CreatePersonsTable();
    

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

          Person newPerson = new Person();
          newPerson.firstName = Firstname;
          newPerson.LastName = Lastname;
          newPerson.codeName = GenerateCodeName();
          newPerson.Age = Age;
          newPerson.Password = password;

          newPerson.insertPersonToTable();
          
          Console.WriteLine("welcome to the system: ");
          Console.WriteLine($"your code name is {codeNames}");
          Console.WriteLine($"your code password is {password}");
          
      }

      private void logIn()
      {
          Console.WriteLine("enter name code code name:");
          string name = Console.ReadLine();
          
          Console.WriteLine("enter password:");
          string password = Console.ReadLine();
          
          dbServices.checkIfPersonExists(name, password);
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
        menu.play_menu();

    }
}