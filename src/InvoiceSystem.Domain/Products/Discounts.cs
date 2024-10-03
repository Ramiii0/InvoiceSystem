using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace InvoiceSystem.Products
{
    public class Discounts : FullAuditedEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public int Discount {  get; set; }
    }
}
