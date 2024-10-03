using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Reports
{
    public class MonthlyEarningsDto
    {
        public decimal TotalAmounts { get; set; }
        public decimal TotalDiscounts { get;  set; }
        public decimal NetAmount { get; set; }
    }
}
