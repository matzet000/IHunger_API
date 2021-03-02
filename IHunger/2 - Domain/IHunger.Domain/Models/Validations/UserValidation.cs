using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models.Validations
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(u => u.Name)
                   .NotEmpty().WithMessage("The {PropertyName} needs to be provided")
                   .Length(4, 50).WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

            RuleFor(u => u.Email)
                   .NotEmpty().WithMessage("The {PropertyName} needs to be provided");

            RuleFor(u => u.PhoneNumber)
                   .NotEmpty().WithMessage("The {PropertyName} needs to be provided");

            RuleFor(u => u.BirthDate)
                    .NotEmpty().WithMessage("The {PropertyName} needs to be provided");
        }
    }
}
