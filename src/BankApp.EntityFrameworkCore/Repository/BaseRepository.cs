using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Interface;
using Microsoft.EntityFrameworkCore;
using BankApp.EntityFrameworkCore; 
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace BankApp.Repository;

public class BaseRepository<T> : EfCoreRepository<BankAppDbContext, T, Guid>, IBaseRepository<T>
    where T : class, IEntity<Guid>
{
    public BaseRepository(IDbContextProvider<BankAppDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<List<T>> GetByUserIdAsync(Guid userId)
    {
        var result = await GetQueryableAsync();
        return await result
            .Where("UserId == @0", userId)
            .ToListAsync();
    }
}