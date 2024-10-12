using InvoiceSystem.Invoices;
using InvoiceSystem.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace InvoiceSystem.InvoiceItems
{
    public class CreateInvoiceItemsDto
    {
       
        public Guid InvoiceId { get; set; }
    

        public Guid ProductId { get; set; }
        
        public int Quantity { get; set; }
       
       
      
    }
}
