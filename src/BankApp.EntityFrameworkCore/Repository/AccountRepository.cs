
using BankApp.Entities;
using BankApp.Interface;
using Volo.Abp.EntityFrameworkCore;
using BankApp.EntityFrameworkCore;
namespace BankApp.Repository;

public class AccountRepository : BaseRepository<Account>,IAccountRepository
{
    public AccountRepository(IDbContextProvider<BankAppDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}