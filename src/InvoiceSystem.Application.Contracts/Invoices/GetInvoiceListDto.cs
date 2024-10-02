using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace InvoiceSystem.Invoices
{
    public class GetInvoiceListDto : PagedAndSortedResultRequestDto
    {
        public int? Filter { get; set; }
    }
}
