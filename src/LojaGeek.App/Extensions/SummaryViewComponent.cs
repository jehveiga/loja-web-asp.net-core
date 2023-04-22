using LojaGeek.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LojaGeek.App.Extensions
{
    // Classe responsável por passar os erros encontrados para View referida através da validação criada na camada de regra de negócios
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotificador _notificador;

        public SummaryViewComponent(INotificador notificador)
        {
            _notificador = notificador;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // FromResult habilita a compatibilidade para se tornar um método async, para ser retornado uma Task async para o método Invoke
            var notificacoes = await Task.FromResult(_notificador.ObterNotificacoes());

            // Adicionando os erros encontrados para ModelState para ser transportado a View através do ViewData 
            notificacoes.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Mensagem)); // Será apresentado no formulário como Erro

            return View();
        }
    }
}
