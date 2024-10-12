using InvoiceSystem.InvoiceItems;
using InvoiceSystem.Invoices;
using InvoiceSystem.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace InvoiceSystem.Reports
{
    public class ReportService : ApplicationService,IReportAppService 
    { 
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceItemRepository _invoiceItemRepository;
       
       
        private readonly IProductRepository _productRepo;
        public ReportService(IInvoiceRepository repository, IInvoiceItemRepository invoiceItemRepository,  IProductRepository productRepo)
        {
            _invoiceRepository = repository;
            _invoiceItemRepository = invoiceItemRepository;
          
           
            _productRepo = productRepo;
        }

        public async Task<PagedResultDto<DiscountReportsDto>> GetDiscountsReport(GetReportList filter)
        {

            var products = (await _productRepo.GetAll()).AsEnumerable()
           .WhereIf(!filter.Productname.IsNullOrWhiteSpace(), p => p.Name.Contains(filter.Productname));
            if (filter.Productname == null && filter.From != DateTime.MinValue)
            {
                products= products
                 .Where(p => p.ProductDiscount.Any(d => d.StartDate >= filter.From && d.EndDate <= filter.To));
            }

     var discouont = products.Select(x => new DiscountReportsDto 
            { ProductName = x.Name,
                NumberOfDiscount = x.ProductDiscount.Count,
                DiscountDetails = x.ProductDiscount.Select(a=> new DiscountDetails
                {
                    Discount =a.Disount,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,

                }).ToList()
            }).ToList();
            int totalCount = discouont.Count;
            return new PagedResultDto<DiscountReportsDto>(totalCount, discouont); 




           

        }

        public async Task<MonthlyEarningsDto> GetMonthlyEarningReport(GetReportList filter)
        {
            if (filter.Month != 0)
            {
                int year = DateTime.Now.Year;
                filter.From= new DateTime(year, filter.Month, 1);
                filter.To = filter.From.AddMonths(1).AddDays(-1);
            }

            var filteredData = (await _invoiceRepository.GetListAsync()).AsEnumerable()
            .WhereIf(filter.From != DateTime.MinValue, a => a.CreationTime >= filter.From && a.CreationTime <= filter.To).ToList();
           
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
        

        public async Task<PagedResultDto<ItemSalesReportDto>> GetItemSalesReport(GetReportList filter)
        {
            /* var query = from product in await _productRepo.GetAll()
                         join
                         invoiceItem in await _invoiceItemRepository.GetListAsync() on product.Id equals invoiceItem.ProductId into InvoiceGroup
                         select
                         new { ProductName =product.Name,ProductId =product.Id, NUmberOfSales =InvoiceGroup.Count() };*/
            var product=( await _productRepo.GetAll())
                .AsEnumerable()
                .WhereIf(!filter.Productname.IsNullOrWhiteSpace(), p => p.Name.Contains(filter.Productname));
         
            var invoiceItem = (await _invoiceItemRepository.GetListAsync())
                .AsEnumerable()
                .WhereIf(filter.From != DateTime.MinValue && filter.To != DateTime.MinValue,
                a => a.CreationTime >= filter.From && a.CreationTime <= filter.To);


            var result = product.GroupJoin(invoiceItem, x => x.Id, z => z.ProductId, (p, i) => new ItemSalesReportDto
            {
                ProductName = p.Name,
                ProductId = p.Id,
                NumberOfSales = i.Sum(b => b.Quantity)
            }).ToList();
           





            return new PagedResultDto<ItemSalesReportDto>(result.Count, result);
           


        }
    }
}
