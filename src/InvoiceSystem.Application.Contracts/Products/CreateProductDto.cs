using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Products
{
    public class CreateProductDto 
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int PartNo { get; set; }
    }
}
