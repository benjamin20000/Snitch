using Snitch;

namespace Snitch;
class Menu
{
      private DbServices dbServices = new DbServices();
      List<string> codeNames = new List<string>();
      
     
      private void creatTables()
      {
        dbServices.CreatePersonsTable();
        dbServices.CreateReportsTable();
      }

      private void snitchErea(long snitch_id)
      {
          Report new_report = Report.createReport(snitch_id, dbServices);
          new_report.insertReportToTable();
      }
      

      private void signUp()
      {
          Person newPerson = Person.createPerson(dbServices);
          
          // if the func create Person calling from sighUp 
          // the person need to created with password
          // for now i will implemnt it by setting the password 
          Console.WriteLine("enter a new password:");
          string password = Console.ReadLine();
          newPerson.Password = password;

          long id = newPerson.insertPersonToTable();
          
          Console.WriteLine("welcome to the system: ");
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
        Console.WriteLine("1 log in");
        Console.WriteLine("2 sign up");
        
        int choice = int.Parse(Console.ReadLine());

        if (choice == 1)
        {
            logIn();
        }
        else if (choice == 2)
        {
            signUp();
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