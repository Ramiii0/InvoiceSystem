using InvoiceSystem.Invoices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace InvoiceSystem.Web.Pages.Invoices
{
    public class EditModel : InvoicePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]

        public Guid Id { get; set; }
        [BindProperty]
        public UpdateInvoiceDto Invoice { get; set; }
        private readonly IInvoiceAppService _invoiceAppService;
        public EditModel(IInvoiceAppService invoiceApp)
        {
            _invoiceAppService = invoiceApp;
        }
        public async void OnGet()
        {
           var dto = await _invoiceAppService.GetAsync(Id);
            
            Invoice = ObjectMapper.Map<InvoiceDto, UpdateInvoiceDto>(dto);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _invoiceAppService.UpdateAsync(Id, Invoice);
            return NoContent();
        }
    }
}
