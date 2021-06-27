using FluentValidation;
using FluentValidation.Results;
using IHunger.Domain.Interfaces;
using IHunger.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Service
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
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
            _notifier.Handle(new Domain.Notifications.Notification(mensagem));
        }

        protected bool Validation<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            NotifyError(validator);

            return false;
        }
    }
}
