using Microsoft.AspNetCore.Builder;
using InvoiceSystem;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<InvoiceSystemWebTestModule>();

public partial class Program
{
}
