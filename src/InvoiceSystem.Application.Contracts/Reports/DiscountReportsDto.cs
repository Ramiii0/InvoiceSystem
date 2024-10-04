using InvoiceSystem.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace InvoiceSystem.Reports
{
    public class DiscountReportsDto 
    {
       public string ProductName { get; set; }
        public int NumberOfDiscount { get; set; }
        public List<DiscountDetails> DiscountDetails { get; set; } = new List<DiscountDetails>();

    }
}
