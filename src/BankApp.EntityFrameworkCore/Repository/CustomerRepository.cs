using BankApp.Entities;
using BankApp.Interface;
using Volo.Abp.EntityFrameworkCore;
using BankApp.EntityFrameworkCore;
namespace BankApp.Repository;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(IDbContextProvider<BankAppDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}