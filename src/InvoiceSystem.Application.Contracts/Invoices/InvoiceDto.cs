using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace InvoiceSystem.Invoices
{
    public class InvoiceDto: FullAuditedEntityDto<Guid>
    {
        public string CustomerName { get; set; }
     
        public int InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetAmount { get; set; }
    }
}
