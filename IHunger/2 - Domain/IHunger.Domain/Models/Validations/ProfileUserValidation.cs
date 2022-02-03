using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Domain.Models.Validations
{
    public class ProfileUserValidation : AbstractValidator<ProfileUser>
    {
        public ProfileUserValidation()
        {
            RuleFor(p => p.BirthDate)
               .NotEmpty().WithMessage("The {PropertyName} needs to be provided");

            RuleFor(p => p.Name)
                   .NotEmpty().WithMessage("The {PropertyName} needs to be provided");

            RuleFor(p => p.LastName)
                   .NotEmpty().WithMessage("The {PropertyName} needs to be provided");

            RuleFor(p => p.Type)
                   .NotNull().WithMessage("The {PropertyName} needs to be provided")
                   .InclusiveBetween(1, 3).WithMessage("Value must be number Beetween 1, 3");
        }
    }
}
