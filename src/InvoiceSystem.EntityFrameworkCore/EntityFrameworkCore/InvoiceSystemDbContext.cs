using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using InvoiceSystem.Invoices;
using InvoiceSystem.Products;

namespace InvoiceSystem.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class InvoiceSystemDbContext :
    AbpDbContext<InvoiceSystemDbContext>,
    ITenantManagementDbContext,
    IIdentityDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItems> InvoicesItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductPricing>  ProductsPricing { get; set; }
    public DbSet<ProductDiscount> ProductsDiscount { get; set; }
    public DbSet<Discounts> Discounts { get; set; }


    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext 
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext .
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public InvoiceSystemDbContext(DbContextOptions<InvoiceSystemDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureTenantManagement();
        builder.ConfigureBlobStoring();


        builder.Entity<Invoice>(x=>
        {
            x.ToTable(InvoiceSystemConsts.DbTablePrefix + "Invoices", InvoiceSystemConsts.DbSchema);
            x.HasMany(a => a.InvoiceItems).WithOne(i=>i.Invoice).HasForeignKey(i=>i.InvoiceId).OnDelete(DeleteBehavior.Restrict);
          x. ConfigureByConvention();
            x.Property(a=>a.InvoiceAmount).HasPrecision(16,2);
            x.Property(a => a.TotalDiscount).HasPrecision(16, 2);
            x.Property(a => a.NetAmount).HasPrecision(16, 2);
            x.Property(a => a.InvoiceNo).ValueGeneratedOnAdd();


        });
        builder.Entity<InvoiceItems>(x =>
        {
            x.ToTable(InvoiceSystemConsts.DbTablePrefix + "InvoiceItems", InvoiceSystemConsts.DbSchema);
            
            x.ConfigureByConvention();
            x.Property(a => a.Price).HasPrecision(16, 2);
            x.Property(a => a.TotalPrice).HasPrecision(16, 2);
            x.Property(a => a.TotalNet).HasPrecision(16, 2);
            x.Property(a => a.DiscountValue).HasPrecision(16, 2);
            x.Property(a => a.TotalNet).HasPrecision(16, 2);

        });
        builder.Entity<Product>(x =>
        {
            x.ToTable(InvoiceSystemConsts.DbTablePrefix + "Products", InvoiceSystemConsts.DbSchema);
            x.HasOne(a => a.ProductPricing).WithOne(i => i.Product).HasForeignKey<ProductPricing>(i => i.ProductId).OnDelete(DeleteBehavior.Restrict);
            x.HasOne(a => a.ProductDiscount).WithOne(i => i.Product).HasForeignKey<ProductDiscount>(i => i.ProductId).OnDelete(DeleteBehavior.Restrict);
            x.ConfigureByConvention();

        });
        builder.Entity<ProductPricing>(x =>
        {
            x.ToTable(InvoiceSystemConsts.DbTablePrefix + "ProductsPricing", InvoiceSystemConsts.DbSchema);
            x.ConfigureByConvention();
            x.Property(a => a.Price).HasPrecision(16, 2);

        });
        builder.Entity<ProductDiscount>(x =>
        {
            x.ToTable(InvoiceSystemConsts.DbTablePrefix + "ProductsDiscount", InvoiceSystemConsts.DbSchema);
            x.ConfigureByConvention();
            
        });
        builder.Entity<Discounts>(x =>
        {
            x.ToTable(InvoiceSystemConsts.DbTablePrefix + "Discounts", InvoiceSystemConsts.DbSchema);
            x.ConfigureByConvention();

        });

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(InvoiceSystemConsts.DbTablePrefix + "YourEntities", InvoiceSystemConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    }
}
