using Volo.Abp.Modularity;

namespace InvoiceSystem;

[DependsOn(
    typeof(InvoiceSystemDomainModule),
    typeof(InvoiceSystemTestBaseModule)
)]
public class InvoiceSystemDomainTestModule : AbpModule
{

}
