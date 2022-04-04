using FluentValidation;
using FluentValidation.Results;
using IHunger.Domain.Interfaces;
using IHunger.Domain.Models;
using IHunger.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Service
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseService(INotifier notifier, IUnitOfWork unitOfWork)
        {
            _notifier = notifier;
            _unitOfWork = unitOfWork;
        }

        protected void NotifyError(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                NotifyError(error.ErrorMessage);
            }
        }

        protected void NotifyError(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }

        protected bool Validate<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            NotifyError(validator);

            return false;
        }
    }
}
