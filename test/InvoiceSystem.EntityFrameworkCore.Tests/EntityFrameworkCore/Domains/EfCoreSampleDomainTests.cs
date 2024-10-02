using InvoiceSystem.Samples;
using Xunit;

namespace InvoiceSystem.EntityFrameworkCore.Domains;

[Collection(InvoiceSystemTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<InvoiceSystemEntityFrameworkCoreTestModule>
{

}
