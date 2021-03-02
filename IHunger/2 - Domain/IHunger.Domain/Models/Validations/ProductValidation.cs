using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(p => p.Price)
               .NotEmpty().WithMessage("The {PropertyName} needs to be provided");

            RuleFor(p => p.Kosher)
               .NotEmpty().WithMessage("The {PropertyName} needs to be provided");

            RuleFor(p => p.Vegan)
              .NotEmpty().WithMessage("The {PropertyName} needs to be provided");

            RuleFor(p => p.Vegetarian)
              .NotEmpty().WithMessage("The {PropertyName} needs to be provided");

            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("The {PropertyName} needs to be provided")
                .Length(4, 50).WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

            RuleFor(a => a.Description)
                .NotEmpty().WithMessage("The {PropertyName} needs to be provided")
                .Length(3, 200).WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");
        }
    }
}
