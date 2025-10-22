using Volo.Abp.Settings;
using BankApp.Enums;
using BankApp.Entities;
namespace BankApp.Settings;

public class BankAppSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        // Kart ayarları
        context.Add(
            new SettingDefinition(
                BankAppSettings.Cards.DefaultCardType,
                ((int)CardType.Debit).ToString()
            ),
            new SettingDefinition(
                BankAppSettings.Cards.AllowedCardTypes,
                $"{(int)CardType.Debit},{(int)CardType.Credit}"
            ),
            new SettingDefinition(
                BankAppSettings.Cards.AllowedCardStatuses,
                $"{(int)CardStatuses.Active},{(int)CardStatuses.Blocked},{(int)CardStatuses.Passive}"
            )
        );
        
        // Transaction ayarları
        context.Add(
            new SettingDefinition(
                BankAppSettings.Transaction.AllowedTransactions,
                $"{(int)TransactionTypes.Deposit}," +
                $"{(int)TransactionTypes.Payment}," +
                $"{(int)TransactionTypes.Purchase}," +
                $"{(int)TransactionTypes.Withdraw}," +
                $"{(int)TransactionTypes.Transfer}"
            )
        );
    }
}