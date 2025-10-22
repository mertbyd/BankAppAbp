// MyAbpProjectNet7/MyAbpProjectNet7DomainErrorCodes.cs (Domain Katmanı)

namespace BankApp;

public static class BankAppDomainErrorCodes
{
   
    private const string Prefix = "BankingSystem:"; // Sizin belirlediğiniz Domain Code Namespace
    public static class General
    {
        public const string ValidationFailed = Prefix + "GENERAL_VALIDATION_FAILED";
        public const string UnknownError = Prefix + "GENERAL_UNKNOWN_ERROR";
        public const string InvalidOperation = Prefix + "GENERAL_INVALID_OPERATION";
    }
    public static class Auth
    {
        public const string InvalidCredentials = Prefix + "AUTH_INVALID_CREDENTIALS";
        public const string AccessDenied = Prefix + "AUTH_ACCESS_DENIED";
        public const string InvalidToken = Prefix + "AUTH_INVALID_TOKEN";
        public const string UserNotAssociatedWithCustomer = Prefix + "AUTH_USER_NO_CUSTOMER"; // BaseAppService için
    }
    public static class Users
    {
        public const string NotFound = Prefix + "USER_NOT_FOUND";
    }
    public static class Customers
    {
        public const string NotFound = Prefix + "CUSTOMER_NOT_FOUND";
        public const string CanNotDelete = Prefix + "CUSTOMER_CAN_NOT_DELETE";
    }
    public static class Accounts
    {
        public const string NotFound = Prefix + "ACCOUNT_NOT_FOUND";
        public const string InsufficientBalance = Prefix + "ACCOUNT_INSUFFICIENT_BALANCE";
        public const string CanNotDelete = Prefix + "ACCOUNT_CAN_NOT_DELETE";
    }
    public static class Cards
    {
        public const string NotFound = Prefix + "CARD_NOT_FOUND";
        public const string UnauthorizedOwnership = Prefix + "CARD_UNAUTHORIZED_OWNERSHIP"; // Kendi kartı değil
        public const string InsufficientLimit = Prefix + "CARD_INSUFFICIENT_LIMIT";
        public const string InvalidCardType = Prefix + "CARD_INVALID_TYPE";
        public const string BlockedCard = Prefix + "CARD_BLOCKED";
        public const string WithdrawalNotAllowed = Prefix + "CARD_WITHDRAWAL_NOT_ALLOWED";
        public const string DepositNotAllowed = Prefix + "CARD_DEPOSIT_NOT_ALLOWED";
        public const string PaymentNotAllowed = Prefix + "CARD_PAYMENT_NOT_ALLOWED";
    }
    public static class Transactions
    {
        public const string NotFound = Prefix + "TRANSACTION_NOT_FOUND";
        public const string InvalidAmount = Prefix + "TRANSACTION_INVALID_AMOUNT";
        public const string AmountLimitExceeded = Prefix + "TRANSACTION_AMOUNT_LIMIT_EXCEEDED";
    }
}