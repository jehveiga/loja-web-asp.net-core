using LojaGeek.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LojaGeek.Business.Notificacoes
{
    public class Notificador : INotificador
    {
        private List<Notificacao> _notificacoes;

        public Notificador()
        {
            _notificacoes = new List<Notificacao>();
        }

        public void Handle(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacoes;
        }

        // Verifica se tem algum erro na lista de notificações para alguma atitude a ser tomada seguindo as regras de negócios da aplicação
        public bool TemNotificacao()
        {
            return _notificacoes.Any();
        }
    }
}
