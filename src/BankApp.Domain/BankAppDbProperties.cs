namespace BankApp;

public static class BankAppDbProperties
{
    public static string DbTablePrefix { get; set; } = "";
    public static string DbSchema { get; set; } = "bankapp";
    public const string ConnectionStringName = "BankApp";

}