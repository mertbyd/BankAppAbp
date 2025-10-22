using System;
using System.Threading.Tasks;
using Volo.Abp.Aspects;
using Volo.Abp.DependencyInjection;
using Volo.Abp.DynamicProxy;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using BankApp.Entities;
using Volo.Abp.Guids;

namespace BankApp.EntityFrameworkCore.Interceptor;
public class GeneralLogInterceptor : AbpInterceptor, ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly IRepository<GeneralLog, Guid> _logRepository;
    public GeneralLogInterceptor(
        IGuidGenerator guidGenerator,
        IRepository<GeneralLog, Guid> logRepository)
    {
        _guidGenerator = guidGenerator;
        _logRepository = logRepository;
    }
    public override async Task InterceptAsync(IAbpMethodInvocation invocation)
    {
  
        await invocation.ProceedAsync();
        await LogAfterExecutionAsync(invocation);
    }

    private async Task LogAfterExecutionAsync(IAbpMethodInvocation invocation)
    {
        
        if (!invocation.Method.Name.StartsWith("Add", StringComparison.OrdinalIgnoreCase) && 
            !invocation.Method.Name.StartsWith("Insert", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }
        
        if (invocation.ReturnValue is Task<bool> boolTask && !await boolTask)
        {
             return;
        }
        try
        {
            var targetType = invocation.TargetObject.GetType().Name.Replace("Repository", "");
            var entityName = targetType.Replace("`1", ""); 
            var operationType = invocation.Method.Name.StartsWith("Add") ? "Create" : "Insert";
            var entityParameter = invocation.Arguments.Length > 0 ? invocation.Arguments[0] : null;
            var log = new GeneralLog(
                _guidGenerator.Create(),
                operationType,
                $"'{entityName}' varlığı oluşturuldu. Metot: {invocation.Method.Name}",
                success: true
            );
            await _logRepository.InsertAsync(log, autoSave: true);
        }
        catch (Exception ex)
        { ;
        }
    }
}