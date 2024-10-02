using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InvoiceSystem.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class InvoiceSystemDbContextFactory : IDesignTimeDbContextFactory<InvoiceSystemDbContext>
{
    public InvoiceSystemDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        InvoiceSystemEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<InvoiceSystemDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new InvoiceSystemDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../InvoiceSystem.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
