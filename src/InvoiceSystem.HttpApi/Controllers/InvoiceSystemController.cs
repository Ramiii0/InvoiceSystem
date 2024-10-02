using InvoiceSystem.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace InvoiceSystem.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class InvoiceSystemController : AbpControllerBase
{
    protected InvoiceSystemController()
    {
        LocalizationResource = typeof(InvoiceSystemResource);
    }
}
