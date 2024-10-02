using Volo.Abp.Modularity;

namespace InvoiceSystem;

/* Inherit from this class for your domain layer tests. */
public abstract class InvoiceSystemDomainTestBase<TStartupModule> : InvoiceSystemTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
