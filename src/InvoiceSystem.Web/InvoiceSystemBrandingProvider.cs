using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using InvoiceSystem.Localization;

namespace InvoiceSystem.Web;

[Dependency(ReplaceServices = true)]
public class InvoiceSystemBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<InvoiceSystemResource> _localizer;

    public InvoiceSystemBrandingProvider(IStringLocalizer<InvoiceSystemResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
