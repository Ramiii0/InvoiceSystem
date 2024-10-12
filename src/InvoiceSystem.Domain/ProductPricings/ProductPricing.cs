using InvoiceSystem.Products;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace InvoiceSystem.ProductPricings
{
    public class ProductPricing : FullAuditedEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public decimal Price { get; set; }
        public PriceDuration PriceDuration { get; set; }


    }
    [Owned]
    public class PriceDuration
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
