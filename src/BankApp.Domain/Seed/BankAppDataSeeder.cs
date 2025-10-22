using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using BankApp.Domain.Lookups;
using BankApp.Enums;
using Volo.Abp.Uow;

namespace BankApp.Data;

public class BankAppDataSeeder : IDataSeeder, ITransientDependency
{
    private readonly IRepository<CardTypeLookup, int> _cardTypeRepository;
    private readonly IRepository<CardStatusLookup, int> _cardStatusRepository;
    private readonly IRepository<TransactionTypeLookup, int> _transactionTypeRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public BankAppDataSeeder(
        IRepository<CardTypeLookup, int> cardTypeRepository,
        IRepository<CardStatusLookup, int> cardStatusRepository,
        IRepository<TransactionTypeLookup, int> transactionTypeRepository,
        IUnitOfWorkManager unitOfWorkManager)
    {
        _cardTypeRepository = cardTypeRepository;
        _cardStatusRepository = cardStatusRepository;
        _transactionTypeRepository = transactionTypeRepository;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (var uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: true))
        {
            await SeedCardTypesAsync();
            await SeedCardStatusesAsync();
            await SeedTransactionTypesAsync();
            
            await uow.CompleteAsync();
        }
    }

    private async Task SeedCardTypesAsync()
    {
        if (await _cardTypeRepository.GetCountAsync() == 0)
        {
            await _cardTypeRepository.InsertAsync(
                new CardTypeLookup(CardType.Debit, "Debit Card", "Vadesiz hesap kartı"),
                autoSave: false
            );
            
            await _cardTypeRepository.InsertAsync(
                new CardTypeLookup(CardType.Credit, "Credit Card", "Kredi kartı"),
                autoSave: false
            );
        }
    }

    private async Task SeedCardStatusesAsync()
    {
        if (await _cardStatusRepository.GetCountAsync() == 0)
        {
            await _cardStatusRepository.InsertAsync(
                new CardStatusLookup(CardStatuses.Active, "Active", "Kart kullanılabilir"),
                autoSave: false
            );
            
            await _cardStatusRepository.InsertAsync(
                new CardStatusLookup(CardStatuses.Passive, "Passive", "Geçici olarak kullanılamaz"),
                autoSave: false
            );
            
            await _cardStatusRepository.InsertAsync(
                new CardStatusLookup(CardStatuses.Blocked, "Blocked", "Güvenlik nedeniyle engellenmiş"),
                autoSave: false
            );
        }
    }

    private async Task SeedTransactionTypesAsync()
    {
        if (await _transactionTypeRepository.GetCountAsync() == 0)
        {
            await _transactionTypeRepository.InsertAsync(
                new TransactionTypeLookup(TransactionTypes.Deposit, "Deposit", "Para Yatırma"),
                autoSave: false
            );
            
            await _transactionTypeRepository.InsertAsync(
                new TransactionTypeLookup(TransactionTypes.Withdraw, "Withdraw", "Para Çekme"),
                autoSave: false
            );
            
            await _transactionTypeRepository.InsertAsync(
                new TransactionTypeLookup(TransactionTypes.Purchase, "Purchase", "Harcama"),
                autoSave: false
            );
            
            await _transactionTypeRepository.InsertAsync(
                new TransactionTypeLookup(TransactionTypes.Payment, "Payment", "Ödeme"),
                autoSave: false
            );
            
            await _transactionTypeRepository.InsertAsync(
                new TransactionTypeLookup(TransactionTypes.Transfer, "Transfer", "Transfer"),
                autoSave: false
            );
        }
    }
}