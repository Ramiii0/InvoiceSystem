using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace InvoiceSystem.Data;

/* This is used if database provider does't define
 * IInvoiceSystemDbSchemaMigrator implementation.
 */
public class NullInvoiceSystemDbSchemaMigrator : IInvoiceSystemDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
