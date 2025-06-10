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