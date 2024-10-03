using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.ProductsDiscount
{
    public class CreateProductDiscountDto
    {
        public Guid ProductId { get; set; }
        [Range(1, 100, ErrorMessage = "The discount must be between 1 and 100.")]
        public int Disount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
