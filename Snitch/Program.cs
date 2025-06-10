using Snitch.db;

class Menu
{
      private DbServices dbServices = new DbServices();
      private void details_validation()
      {
      }
  
      private void creatTables()
      {
        dbServices.CreatePersonsTable();
        Crud crud = new Crud();
        Dictionary<string,object> data = new Dictionary<string, object>(){
        {"firstName", "John"},
        {"lastName", "Doe"},
        {"codeName", "123"}
        };
        crud.InsertRow("persons", data);

      }
    
    private void play_menu()
    {
        Console.WriteLine("Welcome to Snitch!");
        Console.WriteLine("enter name code name:");
        string name = Console.ReadLine();
        Console.WriteLine("enter password:");
        string password = Console.ReadLine();
    }
    static void Main(string[] args)
    {
        Menu menu = new Menu();
        menu.creatTables();
       
    }
}