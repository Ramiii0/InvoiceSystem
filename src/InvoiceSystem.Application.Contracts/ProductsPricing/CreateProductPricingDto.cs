using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.ProductsPricing
{
    public class CreateProductPricingDto
    {
        public Guid ProductId { get; set; }

        public decimal Price { get; set; }
    }
}
