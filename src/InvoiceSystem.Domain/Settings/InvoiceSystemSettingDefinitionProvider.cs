using Volo.Abp.Settings;

namespace InvoiceSystem.Settings;

public class InvoiceSystemSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(InvoiceSystemSettings.MySetting1));
    }
}
