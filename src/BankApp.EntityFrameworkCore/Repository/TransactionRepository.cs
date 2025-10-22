using BankApp.Entities;
using BankApp.Interface;
using Volo.Abp.EntityFrameworkCore;
using BankApp.EntityFrameworkCore;
namespace BankApp.Repository;

public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(IDbContextProvider<BankAppDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}