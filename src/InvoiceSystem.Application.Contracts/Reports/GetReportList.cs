using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace InvoiceSystem.Reports
{
    public class GetReportList : PagedAndSortedResultRequestDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Month { get; set; }
        public string? Productname { get; set; }
    }
}
