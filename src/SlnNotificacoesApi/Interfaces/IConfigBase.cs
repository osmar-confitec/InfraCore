using System;
using System.Collections.Generic;
using System.Text;

namespace SlnNotificacoesApi.Interfaces
{
    public interface IConfigBase<T> : IDisposable where T : class
    {
        bool EValido();

        void ObterModelo(T viewModel);

        IReadOnlyList<Notificacao> NotificacoesBusca { get; }
    }
}
