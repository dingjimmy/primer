using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Primer.SampleApp
{
    public class CustomerValidator : AbstractValidator<CustomerFacade>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.ID).
        }
    }
}
