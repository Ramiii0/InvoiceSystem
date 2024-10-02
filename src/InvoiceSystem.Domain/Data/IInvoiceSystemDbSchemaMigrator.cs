using System.Threading.Tasks;

namespace InvoiceSystem.Data;

public interface IInvoiceSystemDbSchemaMigrator
{
    Task MigrateAsync();
}
