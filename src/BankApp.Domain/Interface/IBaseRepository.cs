using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace BankApp.Interface;

public interface IBaseRepository<T> : IRepository<T, Guid> where T : class,IEntity<Guid>
{
    Task<List<T>> GetByUserIdAsync(Guid userId);
}