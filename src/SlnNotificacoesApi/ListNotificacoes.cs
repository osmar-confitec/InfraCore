using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SlnNotificacoesApi
{
   public class ListNotificacoes<T> :List<T> where T: Notificacao
    {



        public bool TemNotificacoes => this.Any();

        public bool TemInformacoes => this.Any(x => x is Informacao);

        public bool TemAlertas => this.Any(x => x is Alerta);

        public bool TemErros => this.Any(x => x is Erro);

        public int QtdAlertas => this.Count(x => x is Alerta);

        public int QtdInformacoes => this.Count(x => x is Informacao);

        public int QtdErros => this.Count(x => x is Erro);

        public int QtdNotificacoes => this.Count();

    }
}
