using FluentValidation;
using FluentValidation.Results;
using LojaGeek.App.Models;

namespace LojaGeek.Business.Services
{
    public abstract class BaseService
    {
        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string message)
        {
            // Propagar esse erro até a camada de apresentação
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid)
                return true;
            Notificar(validator);
            return false;
        }
    }

}
