using System.Threading.Tasks;
using InvoiceSystem.Localization;
using InvoiceSystem.Permissions;
using InvoiceSystem.MultiTenancy;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;

namespace InvoiceSystem.Web.Menus;

public class InvoiceSystemMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<InvoiceSystemResource>();

        //Home
        context.Menu.AddItem(
            new ApplicationMenuItem(
                InvoiceSystemMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fa fa-home",
                order: 1
            )
        );
        context.Menu.AddItem(
  new ApplicationMenuItem(
      "InvoiceSystem",
      "Invoices",
      icon: "fas fa-file-invoice-dollar"
  ).AddItem(
      new ApplicationMenuItem(
          "AbpClinic.Patients",
          "Invoicelist",
          url: "/invoices"
      )
  )
);
        context.Menu.AddItem(
        new ApplicationMenuItem(
            "InvoiceSystem",
            "Products",
            icon: "fas fa-box"
        ).AddItem(
            new ApplicationMenuItem(
                "AbpClinic.Patients",
                "ProductsList",
                url: "/products"
            )
        )
      );
        context.Menu.AddItem(
new ApplicationMenuItem(
    "InvoiceSystem",
    "Reports",
    icon: "fas fa-sticky-note"
).AddItem(
    new ApplicationMenuItem(
        "InvoiceSystem",
        "Monthly Earing Report",
        url: "/monthlyearingreport"
    )
).AddItem(
    new ApplicationMenuItem(
        "InvoiceSystem",
        "Discount Report",
        url: "/discountreport"
    )
).AddItem(
    new ApplicationMenuItem(
        "InvoiceSystem",
        "Prodcut Sales Report",
        url: "reports"
    )
)
);


        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 5;

        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);
    
        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }
        
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 7);
        
        return Task.CompletedTask;
    }
}
