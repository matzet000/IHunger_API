using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models.Validations
{
    public class RestaurantValidation : AbstractValidator<Restaurant>
    {
        public RestaurantValidation()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("The {PropertyName} needs to be provided")
                .Length(3, 50).WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("The {PropertyName} needs to be provided")
                .Length(3, 200).WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");
        }
    }
}
