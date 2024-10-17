using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Invoices
{
    public class CreateInvoiceValidator : AbstractValidator<CreateInvoiceDto>
    {
        public CreateInvoiceValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().Length(3, 10).WithErrorCode(InvoiceSystemDomainErrorCodes.INVALID_Invoice_DATA_NAME_AR).WithMessage("name is req");
        }
    }
}
