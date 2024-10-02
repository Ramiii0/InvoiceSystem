using InvoiceSystem.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace InvoiceSystem.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(InvoiceSystemEntityFrameworkCoreModule),
    typeof(InvoiceSystemApplicationContractsModule)
)]
public class InvoiceSystemDbMigratorModule : AbpModule
{
}
