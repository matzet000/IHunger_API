using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models.Validations
{
    public class CouponValidation : AbstractValidator<Coupon>
    {
        public CouponValidation()
        {
            RuleFor(c => c.Code)
                .NotEmpty().WithMessage("The {PropertyName} needs to be provided")
                .Length(3, 50).WithMessage("The {PropertyName} need to have between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Value)
                .NotEmpty().WithMessage("The {PropertyName} needs to be provided");

            RuleFor(c => c.ExpireAt)
                .NotEmpty().WithMessage("The {PropertyName} needs to be provided");
        }
    }
}
