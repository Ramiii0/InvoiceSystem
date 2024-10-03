using InvoiceSystem.Invoices;
using InvoiceSystem.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace InvoiceSystem.Reports
{
    public class ReportService : ApplicationService,IReportAppService
    {
        private readonly IRepository<Invoice,Guid> _invoiceRepository;
        private readonly IRepository<InvoiceItems, Guid> _invoiceItemRepository;
        private readonly IRepository<Product, Guid> _productrepo;
        private readonly IRepository<Discounts, Guid> _discount;
        public ReportService(IRepository<Invoice, Guid> repository, IRepository<InvoiceItems, Guid> invoiceItemRepository, IRepository<Product, Guid> productrepo, IRepository<Discounts, Guid> discount)
        {
            _invoiceRepository = repository;
            _invoiceItemRepository = invoiceItemRepository;
            _productrepo = productrepo;
            _discount = discount;
        }

        public async Task<List<DiscountReportsDto>> GetDiscountsReport(DateFilter filter)
        {
            var query = await _discount.GetListAsync();
            var discounts = query.Where(a => a.CreationTime >= filter.From && a.CreationTime <= filter.To);
            var productslist = await _productrepo.GetListAsync();
            List<DiscountReportsDto> discountlist = new List<DiscountReportsDto>();
            foreach (var product in productslist)
            {
                var p =discounts.Where(x => x.ProductId == product.Id);
                foreach (var item in p)
                {
                    DiscountReportsDto discountReportsDto = new DiscountReportsDto()
                    {
                        ProductName = product.Name,
                        DiscountDetails = item.Discount,

                    };
                    discountReportsDto.NumberOfDiscount=p.Count();
                    discountlist.Add(discountReportsDto);





                }
            }
            return discountlist;

        }

        public async Task<MonthlyEarningsDto> GetMonthlyReport(DateFilter filter)
        {
           var invoice = await _invoiceRepository.GetListAsync();
            var filteredData = invoice.Where(a => a.CreationTime >= filter.From && a.CreationTime <= filter.To);
            decimal totalAmount= 0;
            decimal totalaDicsount = 0;
            decimal netAmount = 0;
            foreach(var item in filteredData)
            {
                totalAmount +=  item.InvoiceAmount;
                totalaDicsount += item.TotalDiscount;
            }
            netAmount= totalAmount-totalaDicsount;
            MonthlyEarningsDto response = new MonthlyEarningsDto()
            {
                TotalAmounts = totalAmount,
                TotalDiscounts = totalaDicsount,
                NetAmount = netAmount
            };
            return response;
        }
        

        public async Task<List<ItemSalesReportDto>> GetItemSalesReport(DateFilter filter)
        {
            var query = await _invoiceItemRepository.GetListAsync();
            var invoiceItems = query.Where( a=> a.CreationTime >= filter.From && a.CreationTime <= filter.To);
            var productslist = await _productrepo.GetListAsync();
            List<ItemSalesReportDto> productsSales = new List< ItemSalesReportDto>();
            foreach( var item in productslist)
            {
               var product= invoiceItems.Where(x => x.ProductId == item.Id);
                
                    var newProduct = new ItemSalesReportDto()
                    {
                        ProductId = item.Id,
                        ProductName = item.Name,
                        NumberOfSales = product.Count()

                    };
                    productsSales.Add(newProduct);
                

            }
            return productsSales;

        }
    }
}
