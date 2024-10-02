using Xunit;

namespace InvoiceSystem.EntityFrameworkCore;

[CollectionDefinition(InvoiceSystemTestConsts.CollectionDefinitionName)]
public class InvoiceSystemEntityFrameworkCoreCollection : ICollectionFixture<InvoiceSystemEntityFrameworkCoreFixture>
{

}
