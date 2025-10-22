using System;
using BankApp.Repository;
using Volo.Abp.DependencyInjection;
using Volo.Abp.DynamicProxy;
using Volo.Abp.Domain.Repositories;
namespace BankApp.EntityFrameworkCore.Interceptor;
public static class GeneralLogInterceptorRegistrar
{
    public static void RegisterIfNeeded(IOnServiceRegistredContext context)
    {
        if (context.ImplementationType.IsAssignableTo(typeof(BaseRepository<>))) 
        {
            context.Interceptors.TryAdd<GeneralLogInterceptor>();
        }
    }
}