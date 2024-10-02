using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace InvoiceSystem.InvoiceItem
{
    public class UpdateInvoiceItemsDto 
    {
        public Guid InvoiceId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
