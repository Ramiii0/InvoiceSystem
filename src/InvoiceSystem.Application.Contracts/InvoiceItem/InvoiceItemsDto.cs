using InvoiceSystem.Invoices;
using InvoiceSystem.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace InvoiceSystem.InvoiceItem
{
    public class InvoiceItemsDto : FullAuditedEntityDto<Guid>
    {
        public Guid InvoiceId { get; set; }
       // public Invoice Invoice { get; set; }
        public Guid ProductId { get; set; }
     
        public int Quantity { get; set; }
        public Guid PriceId { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid DiscountId { get; set; }

        public decimal DiscountValue { get; set; }
        public decimal TotalNet { get; set; }
    }
}
