using InvoiceSystem.Localization;
using Volo.Abp.Application.Services;

namespace InvoiceSystem;

/* Inherit your application services from this class.
 */
public abstract class InvoiceSystemAppService : ApplicationService
{
    protected InvoiceSystemAppService()
    {
        LocalizationResource = typeof(InvoiceSystemResource);
    }
}
