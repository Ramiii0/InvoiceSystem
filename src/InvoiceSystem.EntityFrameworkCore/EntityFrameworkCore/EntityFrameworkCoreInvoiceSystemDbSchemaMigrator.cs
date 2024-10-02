using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using InvoiceSystem.Data;
using Volo.Abp.DependencyInjection;

namespace InvoiceSystem.EntityFrameworkCore;

public class EntityFrameworkCoreInvoiceSystemDbSchemaMigrator
    : IInvoiceSystemDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreInvoiceSystemDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the InvoiceSystemDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<InvoiceSystemDbContext>()
            .Database
            .MigrateAsync();
    }
}
