using InvoiceSystem.Products;
using InvoiceSystem.ProductsDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace InvoiceSystem.Web.Pages.Products
{
    public class AddDiscountModel : InvoicePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]

        public Guid Id { get; set; }
        [BindProperty]
        public CreateProductDiscountDto Discount { get; set; }
        private readonly IProductDiscountAppService _productdiscountAppService;
        public AddDiscountModel(IProductDiscountAppService productDiscountAppService)
        {
            _productdiscountAppService = productDiscountAppService;
            
        }
        public void OnGet()
        {
            Discount = new CreateProductDiscountDto();
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Discount.ProductId = Id;
            await _productdiscountAppService.CreateAsync(Discount);
            return NoContent();
        }
    }
}
