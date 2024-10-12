using InvoiceSystem.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace InvoiceSystem.ProductsPricings
{
    public class ProductPricingDto : FullAuditedEntityDto<Guid>
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }

        public decimal Price { get; set; }
       

    }
}
