namespace BankApp.Permissions;

public static class BankAppPermissions
{
    // Ana grup adı
    public const string GroupName = "BankApp";

    // Customer  İzinleri
    public static class Customer
    {
        public const string Default = GroupName + ".Customer";
        public const string Read = Default + ".Read";
        public const string Write = Default + ".Write";
        public const string Delete = Default + ".Delete";
        public const string ViewAll = Default + ".ViewAll";
    }

    // Account  İzinleri
    public static class Account
    {
        public const string Default = GroupName + ".Account";
        public const string Read = Default + ".Read";
        public const string Write = Default + ".Write";
        public const string Delete = Default + ".Delete";
        public const string Transfer = Default + ".Transfer";
    }

    // Card İzinleri
    public static class Card
    {
        public const string Default = GroupName + ".Card";
        public const string Read = Default + ".Read";
        public const string Write = Default + ".Write";
        public const string Delete = Default + ".Delete";
        public const string Purchase = Default + ".Purchase";
        public const string Withdraw = Default + ".Withdraw";
        public const string Deposit = Default + ".Deposit";
    }

    // Transaction İzinleri
    public static class Transaction
    {
        public const string Default = GroupName + ".Transaction";
        public const string Read = Default + ".Read";
        public const string Write = Default + ".Write";
        public const string ViewAll = Default + ".ViewAll";
    }

    // ROLE YÖNETİMİ İZİNLERİ 
    public static class RoleManagement
    {
        public const string Default = GroupName + ".RoleManagement";
        public const string Create = Default + ".Create";      // Yeni rol oluşturma
        public const string Update = Default + ".Update";      // Rol güncelleme
        public const string Delete = Default + ".Delete";      // Rol silme
        public const string ViewAll = Default + ".ViewAll";    // Tüm rolleri görme
        public const string ManagePermissions = Default + ".ManagePermissions"; // Role permission atama
    }

    // USER YÖNETİMİ İZİNLERİ 
    public static class UserManagement
    {
        public const string Default = GroupName + ".UserManagement";
        public const string Create = Default + ".Create";      // Yeni kullanıcı oluşturma
        public const string Update = Default + ".Update";      // Kullanıcı güncelleme
        public const string Delete = Default + ".Delete";      // Kullanıcı silme
        public const string ViewAll = Default + ".ViewAll";    // Tüm kullanıcıları görme
        public const string ManageRoles = Default + ".ManageRoles";         // User'a rol atama
        public const string ManagePermissions = Default + ".ManagePermissions"; // User'a özel permission atama
    }

    // PERMISSION YÖNETİMİ İZİNLERİ - Admin için
    public static class PermissionManagement
    {
        public const string Default = GroupName + ".PermissionManagement";
        public const string Grant = Default + ".Grant";        // Permission verme
        public const string Revoke = Default + ".Revoke";      // Permission kaldırma
        public const string ViewAll = Default + ".ViewAll";    // Tüm permission'ları görme
    }
}