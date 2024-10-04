using InvoiceSystem.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace InvoiceSystem.Permissions;

public class InvoiceSystemPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        

        //Define your own permissions here. Example:
        //myGroup.AddPermission(InvoiceSystemPermissions.MyPermission1, L("Permission:MyPermission1"));

        var Invoice = context.AddGroup(InvoiceSystemPermissions.GroupName, L("Permission:Invoice"));
        var booksPermission = Invoice.AddPermission(InvoiceSystemPermissions.Invoices.Default, L("Permission:Invoices"));
        booksPermission.AddChild(InvoiceSystemPermissions.Invoices.Create, L("Permission:Invoice.Create"));
        booksPermission.AddChild(InvoiceSystemPermissions.Invoices.Edit, L("Permission:Invoice.Edit"));
        booksPermission.AddChild(InvoiceSystemPermissions.Invoices.Delete, L("Permission:Invoice.Delete"));
        booksPermission.AddChild(InvoiceSystemPermissions.Invoices.RestoreDelete, L("Permission:Invoice.RestoreDelete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<InvoiceSystemResource>(name);
    }
}
