using BankApp.Entities;
using BankApp.Interface;
using Volo.Abp.EntityFrameworkCore;
using BankApp.EntityFrameworkCore;
namespace BankApp.Repository;

public class CardRepository : BaseRepository<Card>, ICardRepository
{
    public CardRepository(IDbContextProvider<BankAppDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}