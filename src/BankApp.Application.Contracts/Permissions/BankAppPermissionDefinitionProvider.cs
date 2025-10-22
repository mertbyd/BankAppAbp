using BankApp.Localization; // Bu using ifadesinin doğru BankApp.Localization namespace'ini içerdiğinden emin olun
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace BankApp.Permissions;

// İsim tutarlılığı için sınıf adını BankApp ile başlattım.
public class BankAppPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        // Ana grubu oluştur - UI'da "BankApp" başlığı altında görünecek
        var myGroup = context.AddGroup(BankAppPermissions.GroupName);

        // CUSTOMER (MÜŞTERİ) İZİNLERİ
        var customerPermission = myGroup.AddPermission(
            BankAppPermissions.Customer.Default,
            L("Permission:Customer")
        );
        customerPermission.AddChild(
            BankAppPermissions.Customer.Read,
            L("Permission:Customer.Read")
        );
        customerPermission.AddChild(
            BankAppPermissions.Customer.Write,
            L("Permission:Customer.Write")
        );
        customerPermission.AddChild(
            BankAppPermissions.Customer.Delete,
            L("Permission:Customer.Delete")
        );
        customerPermission.AddChild(
            BankAppPermissions.Customer.ViewAll,
            L("Permission:Customer.ViewAll")
        );
        // ACCOUNT (HESAP) İZİNLERİ
        var accountPermission = myGroup.AddPermission(
            BankAppPermissions.Account.Default,
            L("Permission:Account")
        );
        accountPermission.AddChild(
            BankAppPermissions.Account.Read,
            L("Permission:Account.Read")
        );
        accountPermission.AddChild(
            BankAppPermissions.Account.Write,
            L("Permission:Account.Write")
        );
        accountPermission.AddChild(
            BankAppPermissions.Account.Delete,
            L("Permission:Account.Delete")
        );
        accountPermission.AddChild(
            BankAppPermissions.Account.Transfer,
            L("Permission:Account.Transfer")
        );
        // CARD (KART) İZİNLERİ
        var cardPermission = myGroup.AddPermission(
            BankAppPermissions.Card.Default,
            L("Permission:Card")
        );
        cardPermission.AddChild(
            BankAppPermissions.Card.Read,
            L("Permission:Card.Read")
        );
        cardPermission.AddChild(
            BankAppPermissions.Card.Write,
            L("Permission:Card.Write")
        );
        cardPermission.AddChild(
            BankAppPermissions.Card.Delete,
            L("Permission:Card.Delete")
        );
        cardPermission.AddChild(
            BankAppPermissions.Card.Purchase,
            L("Permission:Card.Purchase")
        );
        cardPermission.AddChild(
            BankAppPermissions.Card.Withdraw,
            L("Permission:Card.Withdraw")
        );
        cardPermission.AddChild(
            BankAppPermissions.Card.Deposit,
            L("Permission:Card.Deposit")
        );
        // TRANSACTION (İŞLEM) İZİNLERİ
        var transactionPermission = myGroup.AddPermission(
            BankAppPermissions.Transaction.Default,
            L("Permission:Transaction")
        );
        transactionPermission.AddChild(
            BankAppPermissions.Transaction.Read,
            L("Permission:Transaction.Read")
        );
        transactionPermission.AddChild(
            BankAppPermissions.Transaction.Write,
            L("Permission:Transaction.Write")
        );
        transactionPermission.AddChild(
            BankAppPermissions.Transaction.ViewAll,
            L("Permission:Transaction.ViewAll")
        );
        // ROLE YÖNETİMİ İZİNLERİ - Admin için
        var roleManagementPermission = myGroup.AddPermission(
            BankAppPermissions.RoleManagement.Default,
            L("Permission:RoleManagement")
        );
        roleManagementPermission.AddChild(
            BankAppPermissions.RoleManagement.Create,
            L("Permission:RoleManagement.Create")
        );
        roleManagementPermission.AddChild(
            BankAppPermissions.RoleManagement.Update,
            L("Permission:RoleManagement.Update")
        );
        roleManagementPermission.AddChild(
            BankAppPermissions.RoleManagement.Delete,
            L("Permission:RoleManagement.Delete")
        );
        roleManagementPermission.AddChild(
            BankAppPermissions.RoleManagement.ViewAll,
            L("Permission:RoleManagement.ViewAll")
        );
        roleManagementPermission.AddChild(
            BankAppPermissions.RoleManagement.ManagePermissions,
            L("Permission:RoleManagement.ManagePermissions")
        );
        // USER YÖNETİMİ İZİNLERİ - Admin için
        var userManagementPermission = myGroup.AddPermission(
            BankAppPermissions.UserManagement.Default,
            L("Permission:UserManagement")
        );
        userManagementPermission.AddChild(
            BankAppPermissions.UserManagement.Create,
            L("Permission:UserManagement.Create")
        );
        userManagementPermission.AddChild(
            BankAppPermissions.UserManagement.Update,
            L("Permission:UserManagement.Update")
        );
        userManagementPermission.AddChild(
            BankAppPermissions.UserManagement.Delete,
            L("Permission:UserManagement.Delete")
        );
        userManagementPermission.AddChild(
            BankAppPermissions.UserManagement.ViewAll,
            L("Permission:UserManagement.ViewAll")
        );
        userManagementPermission.AddChild(
            BankAppPermissions.UserManagement.ManageRoles,
            L("Permission:UserManagement.ManageRoles")
        );
        userManagementPermission.AddChild(
            BankAppPermissions.UserManagement.ManagePermissions,
            L("Permission:UserManagement.ManagePermissions")
        );
        // PERMISSION YÖNETİMİ İZİNLERİ - Admin için
        var permissionManagementPermission = myGroup.AddPermission(
            BankAppPermissions.PermissionManagement.Default,
            L("Permission:PermissionManagement")
        );
        permissionManagementPermission.AddChild(
            BankAppPermissions.PermissionManagement.Grant,
            L("Permission:PermissionManagement.Grant")
        );
        permissionManagementPermission.AddChild(
            BankAppPermissions.PermissionManagement.Revoke,
            L("Permission:PermissionManagement.Revoke")
        );
        permissionManagementPermission.AddChild(
            BankAppPermissions.PermissionManagement.ViewAll,
            L("Permission:PermissionManagement.ViewAll")
        );
    }

    // Lokalizasyon helper metodu - KRİTİK DÜZELTME
    // 'BankAppResource' sınıfını kullandığınızdan emin olun.
    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BankAppResource>(name);
    }
}
