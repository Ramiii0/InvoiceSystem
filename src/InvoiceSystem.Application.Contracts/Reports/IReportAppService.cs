using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace InvoiceSystem.Reports
{
    public interface IReportAppService
    {
        Task<MonthlyEarningsDto> GetMonthlyEarningReport(GetReportList filter);
        Task<PagedResultDto<DiscountReportsDto>> GetDiscountsReport(GetReportList filter);
        Task<PagedResultDto<ItemSalesReportDto>> GetItemSalesReport(GetReportList filter);

    }
}
