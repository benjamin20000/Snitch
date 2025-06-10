namespace Snitch.db;

public class DbServices
{
    string schemaQuery =
        "id int primery key incrise, " +
        "firstName string, " +
        "lastName string, " +
        "codeName string, " +
        "password string, " +
        "address string, " +
        "phoneNumber string, " +
        "email string, ";
    crud.CreateTable("persons",schemaQuery);
}