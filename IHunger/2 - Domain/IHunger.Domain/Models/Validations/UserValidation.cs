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

            RuleFor(u => u.Email)
                   .NotEmpty().WithMessage("The {PropertyName} needs to be provided");

            RuleFor(u => u.PhoneNumber)
                   .NotEmpty().WithMessage("The {PropertyName} needs to be provided");
        }
    }
}
