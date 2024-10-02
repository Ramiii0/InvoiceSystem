using Volo.Abp.Modularity;

namespace InvoiceSystem;

[DependsOn(
    typeof(InvoiceSystemApplicationModule),
    typeof(InvoiceSystemDomainTestModule)
)]
public class InvoiceSystemApplicationTestModule : AbpModule
{

}
