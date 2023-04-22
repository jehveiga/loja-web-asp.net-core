using FluentValidation;
using FluentValidation.Results;
using LojaGeek.App.Models;
using LojaGeek.Business.Interfaces;
using LojaGeek.Business.Notificacoes;

namespace LojaGeek.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        // Notificando os erros encontrados na entidade, informando os erros encontrados por mensagem
        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        // Validando a entidade passada como parâmetro
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
