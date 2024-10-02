using InvoiceSystem.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace InvoiceSystem.Permissions;

public class InvoiceSystemPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(InvoiceSystemPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(InvoiceSystemPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<InvoiceSystemResource>(name);
    }
}
