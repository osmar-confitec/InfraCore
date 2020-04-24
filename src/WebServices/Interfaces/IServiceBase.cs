using SlnNotificacoesApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebServices.Interfaces
{
    public interface IServiceBase : IDisposable
    {

        List<Notificacao> notificacoes { get; set; }

    }
}
