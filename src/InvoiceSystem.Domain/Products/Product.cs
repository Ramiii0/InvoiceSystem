using InvoiceSystem.InvoiceItems;
using InvoiceSystem.ProductDiscounts;
using InvoiceSystem.ProductPricings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace InvoiceSystem.Products
{
    public class Product : FullAuditedEntity<Guid>
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int PartNo { get; set; }
        [JsonIgnore]
        public InvoiceItem InvoiceItem { get; set; }
        [JsonIgnore]
        public  List<ProductPricing> ProductPricing { get; set; }
        [JsonIgnore]
        public List<ProductDiscount> ProductDiscount { get; set; }

    }
}
