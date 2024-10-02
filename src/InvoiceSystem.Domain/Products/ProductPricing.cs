using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace InvoiceSystem.Products
{
    public class ProductPricing : FullAuditedEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        
        public decimal Price { get; set; }
       
    }
}
