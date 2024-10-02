using Volo.Abp.Modularity;

namespace InvoiceSystem;

public abstract class InvoiceSystemApplicationTestBase<TStartupModule> : InvoiceSystemTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
