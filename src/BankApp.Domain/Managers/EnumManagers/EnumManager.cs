using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Enums;
using BankApp.Settings;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;
using Volo.Abp; // BusinessException için eklendi
using BankApp; // MyAbpProjectNet7DomainErrorCodes için eklendi
namespace BankApp.Managers.EnumManagers;
public class EnumManager : DomainService
{
    private readonly ISettingManager _settingManager;
    public EnumManager(ISettingManager settingManager)
    {
        _settingManager = settingManager;
    }
    // Base method - tüm setting işlemleri için
    private async Task<List<T>> GetEnumListSettingAsync<T>(string settingKey) where T : Enum
    {
        var result = await _settingManager.GetOrNullGlobalAsync(settingKey);
        if (string.IsNullOrEmpty(result))
        {
            throw new BusinessException(BankAppDomainErrorCodes.General.ValidationFailed);
        }

        try
        {
            return result.Split(",")
                .Select(x => (T)Enum.Parse(typeof(T), x.Trim()))
                .ToList();
        }
        catch (Exception ex)
        {
            // Parsing hatası -> General.UnknownError
            throw new BusinessException(BankAppDomainErrorCodes.General.UnknownError);
        }
    }
    #region CardType Setting
    // Default card type getir
    public async Task<CardType> GetDefaultCardTypeAsync()
    {
        var value = await _settingManager.GetOrNullGlobalAsync(BankAppSettings.Cards.DefaultCardType);
        if (string.IsNullOrEmpty(value))
        {
            return CardType.Debit; 
        }
        try
        {
            return (CardType)int.Parse(value);
        }
        catch (FormatException ex)
        {
            throw new BusinessException(BankAppDomainErrorCodes.General.UnknownError);
        }
    }
    // Tüm kart tiplerini getirir
    public async Task<List<CardType>> GetAllowedCardTypesAsync()
        => await GetEnumListSettingAsync<CardType>(BankAppSettings.Cards.AllowedCardTypes);
    public async Task<bool> IsCardTypeAllowedAsync(CardType cardType)
    {
        var allowedTypes = await GetAllowedCardTypesAsync();
        return allowedTypes.Contains(cardType);
    }
    #endregion
    #region CardStatus Setting
    // Tüm kart durumlarını getirir
    public async Task<List<CardStatuses>> GetAllowedCardStatusesAsync()
        => await GetEnumListSettingAsync<CardStatuses>(BankAppSettings.Cards.AllowedCardStatuses);
    public async Task<bool> IsCardStatusAllowedAsync(CardStatuses cardStatus)
    {
        var allowedStatuses = await GetAllowedCardStatusesAsync();
        return allowedStatuses.Contains(cardStatus);
    }
    #endregion
    #region TransactionType Setting
    // Tüm işlem tiplerini getirir
    public async Task<List<TransactionTypes>> GetTransactionTypesAsync()
        => await GetEnumListSettingAsync<TransactionTypes>(BankAppSettings.Transaction.AllowedTransactions);
    public async Task<bool> IsTransactionTypeAllowedAsync(TransactionTypes transactionType)
    {
        var allowedTypes = await GetTransactionTypesAsync();
        return allowedTypes.Contains(transactionType);
    }
    #endregion
}