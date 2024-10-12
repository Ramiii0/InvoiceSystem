using InvoiceSystem.Invoices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace InvoiceSystem.Web.Pages.Invoices
{
    public class CreateModel : InvoicePageModel
    {
        [BindProperty]
        public CreateInvoiceDto Invoice { get; set; }
        private readonly IInvoiceAppService _invoiceAppService;
        public CreateModel(IInvoiceAppService invoice)
        {
            _invoiceAppService = invoice;
        }
        public void OnGet()
        {
            Invoice = new CreateInvoiceDto();
        }
        public async Task<IActionResult> OnPostAsync()
        {
           // await _invoiceAppService.CreateAsync(Invoice);
            return NoContent();
        }
    }
}
