namespace BankApp.Settings;
public static class BankAppSettings
{
    private const string Prefix = "BankApp";
    public static class Cards
    {
        public const string DefaultCardType = Prefix + ".Cards.DefaultCardType";
        public const string AllowedCardTypes = Prefix + ".Cards.AllowedCardTypes";
        public const string AllowedCardStatuses = Prefix + ".Cards.AllowedCardStatuses";
    }
    public static class Role
    {
        public const string AllowedRoles = Prefix + ".Roles.AllowedRoles";
    }
    public static class Transaction
    {
        public const string AllowedTransactions = Prefix + ".Transactions.AllowedTransactions";
    }
}