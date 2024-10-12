using InvoiceSystem.Invoices;
using InvoiceSystem.ProductPricings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace InvoiceSystem.Products
{
    public class ProductsDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int PartNo { get; set; }

        public ProductPriceDto PriceDetails { get; set; }
        public DiscountDetails DiscountDetails { get; set; }
        


    }
    public class ProductPriceDto
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public PriceDuration PriceDuration { get; set; }
    }
    public class DiscountDetails
    {
        public Guid Id { get; set; }
        public int Discount { get; set; }
    }
}
