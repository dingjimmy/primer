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
            RuleFor(c => c.ID).GreaterThan(0);

            RuleFor(c => c.FamilyName)
                .Length(0, 20)
                .Must(x => x == "Poo")
                .WithMessage("Wrong Name!");
        }
    }
}
