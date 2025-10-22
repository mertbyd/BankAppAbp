using BankApp.Entities;
using BankApp.Interface;
using Volo.Abp.EntityFrameworkCore;
using BankApp.EntityFrameworkCore;
namespace BankApp.Repository;

public class GeneralLogRepository : BaseRepository<GeneralLog>, IGeneralLogRepository
{
    public GeneralLogRepository(IDbContextProvider<BankAppDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}