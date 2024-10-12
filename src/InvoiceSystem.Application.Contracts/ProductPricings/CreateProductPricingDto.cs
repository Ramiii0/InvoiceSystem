using InvoiceSystem.ProductPricings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.ProductsPricings
{
    public class CreateProductPricingDto
    {
        public Guid ProductId { get; set; }

        public decimal Price { get; set; }
        public PriceDuration PriceDuration { get; set; }
    }
}
