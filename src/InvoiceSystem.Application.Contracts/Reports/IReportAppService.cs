using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Reports
{
    public interface IReportAppService
    {
        Task<MonthlyEarningsDto> GetMonthlyEarningReport(DateFilter filter);
        Task<List<DiscountReportsDto>> GetDiscountsReport(DateFilter filter);
        Task<List<ItemSalesReportDto>> GetItemSalesReport(DateFilter filter);

    }
}
