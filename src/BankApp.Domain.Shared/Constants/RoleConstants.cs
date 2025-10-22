using System;
namespace BankApp.Constants;

public static class RoleConstants
{
    // Role Names
    public const string Customer = "Customer";
    public const string Teller = "Teller";
    public const string BranchManager = "BranchManager";
    public const string Admin = "Admin";
    // Default role
    public const string DefaultRoleName = Customer;
    public static bool IsValidRoleName(string roleName)
    {
        return roleName == Customer || 
               roleName == Teller || 
               roleName == BranchManager || 
               roleName == Admin;
    }
}