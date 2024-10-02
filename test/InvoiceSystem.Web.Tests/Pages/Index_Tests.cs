using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace InvoiceSystem.Pages;

[Collection(InvoiceSystemTestConsts.CollectionDefinitionName)]
public class Index_Tests : InvoiceSystemWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
