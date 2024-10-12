using InvoiceSystem.InvoiceItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace InvoiceSystem.Invoices
{
    public class Invoice : FullAuditedEntity<Guid>
    {
        public string CustomerName { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

        public int InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetAmount { get; set; }
    }
}
