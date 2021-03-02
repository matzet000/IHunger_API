using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models.Validations
{
    public class RatingValidation : AbstractValidator<Rating>
    {
        public RatingValidation()
        {
            RuleFor(r => r.Starts)
              .NotEmpty().WithMessage("The {PropertyName} needs to be provided");
        }
    }
}
