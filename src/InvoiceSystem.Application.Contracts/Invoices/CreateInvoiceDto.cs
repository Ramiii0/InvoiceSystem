using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Invoices
{
    public class CreateInvoiceDto
    {
        [StringLength(100)]
        public string CustomerName { get; set; }
       

      
       
  
    }
}
