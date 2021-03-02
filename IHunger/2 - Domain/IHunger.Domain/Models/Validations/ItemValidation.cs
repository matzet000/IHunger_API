using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models.Validations
{
    public class ItemValidation : AbstractValidator<Item>
    {
        public ItemValidation()
        {
            RuleFor(i => i.Price)
               .NotEmpty().WithMessage("The {PropertyName} needs to be provided");

            RuleFor(i => i.Quantity)
                .NotEmpty().WithMessage("The {PropertyName} needs to be provided");
        }
    }
}
