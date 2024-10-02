using InvoiceSystem.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace InvoiceSystem.Products
{
    public class ProductsDto :FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int PartNo { get; set; }
        public InvoiceItems InvoiceItem { get; set; }
        public decimal ProductPricingPrice { get; set; }
        public Guid ProductPricingId { get; set; }

        public Guid ProductDiscountId { get; set; }
        public int ProductDiscountDisount { get; set; }
    }
}
