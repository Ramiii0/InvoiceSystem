using InvoiceSystem.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Reports
{
    public class ItemSalesReportDto
    {
        public string ProductName { get; set; }
       public Guid ProductId { get; set; }
        public Int64 NumberOfSales { get; set; }
    }
}
