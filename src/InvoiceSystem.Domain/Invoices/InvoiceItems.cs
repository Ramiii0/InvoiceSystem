using InvoiceSystem.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace InvoiceSystem.Invoices
{
    public class InvoiceItems : FullAuditedEntity<Guid>
    {

        public Guid InvoiceId { get; set; }
        [JsonIgnore]
        public Invoice Invoice { get; set; }
        public Guid ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public Guid PriceId { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid DiscountId { get; set; }
        
        public decimal DiscountValue { get; set; }
        public decimal TotalNet { get; set; }
    }
}
