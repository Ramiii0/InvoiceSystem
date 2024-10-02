using InvoiceSystem.Samples;
using Xunit;

namespace InvoiceSystem.EntityFrameworkCore.Applications;

[Collection(InvoiceSystemTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<InvoiceSystemEntityFrameworkCoreTestModule>
{

}
