using InvoiceSystem.Invoices;
using InvoiceSystem.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace InvoiceSystem.Web.Pages.Products
{
    public class CreateModel : InvoicePageModel
    {
        [BindProperty]
        public CreateProductDto Product { get; set; }
        private readonly IProductAppService _productAppService;
        public CreateModel(IProductAppService product)
        {
            _productAppService = product;
        }
        public void OnGet()
        {
            Product = new CreateProductDto();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _productAppService.CreateAsync(Product);
            return NoContent();
        }
    }
}
