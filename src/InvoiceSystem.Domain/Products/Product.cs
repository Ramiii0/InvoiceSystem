using InvoiceSystem.Invoices;
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
        public InvoiceItems InvoiceItem { get; set; }
        [JsonIgnore]
        public ProductPricing ProductPricing { get; set; }
        [JsonIgnore]
        public ProductDiscount ProductDiscount { get; set; }

    }
}
