
using InvoiceSystem.Products;
using InvoiceSystem.ProductsPricing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace InvoiceSystem.Web.Pages.Products
{
    public class AddPriceModel : InvoicePageModel
    {

        [HiddenInput]
        [BindProperty(SupportsGet = true)]

        public Guid Id { get; set; }
        [BindProperty]
        public CreateProductPricingDto Price { get; set; }
        private readonly IProductPricingAppService _productPricingAppService;
        public AddPriceModel(IProductPricingAppService productPricingAppService)
        {
            _productPricingAppService = productPricingAppService;
        }

            public void OnGet()
        {
            Price = new CreateProductPricingDto();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Price.ProductId = Id;
            await _productPricingAppService.CreateAsync(Price);
            return NoContent();
        }
    }
}
