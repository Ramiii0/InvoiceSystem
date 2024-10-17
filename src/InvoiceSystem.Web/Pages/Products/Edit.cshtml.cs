using InvoiceSystem.Invoices;
using InvoiceSystem.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace InvoiceSystem.Web.Pages.Products
{
    public class EditModel : InvoicePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]

        public Guid Id { get; set; }
        [BindProperty]
        public UpdateProductDto Product { get; set; }
        private readonly IProductAppService _product;
        public EditModel(IProductAppService product)
        {
            _product = product;
        }
        public async void OnGet()
        {
            var dto= await _product.GetAsync(Id);
            Product = ObjectMapper.Map<ProductDto, UpdateProductDto>(dto);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _product.UpdateAsync(Id, Product);
            return NoContent();
        }
    }
}
