using InvoiceSystem.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace InvoiceSystem.Web.Pages;

public abstract class InvoiceSystemPageModel : AbpPageModel
{
    protected InvoiceSystemPageModel()
    {
        LocalizationResourceType = typeof(InvoiceSystemResource);
    }
}
