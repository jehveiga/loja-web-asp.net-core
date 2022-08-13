using LojaGeek.Business.Notificacoes;
using System.Collections.Generic;

namespace LojaGeek.Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();

        List<Notificacao> ObterNotificacoes();

        void Handle(Notificacao notificacao);


    }
}
