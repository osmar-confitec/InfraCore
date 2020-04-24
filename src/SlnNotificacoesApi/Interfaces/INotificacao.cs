using SlnNotificacoesApi.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlnNotificacoesApi.Interfaces
{
   public   interface INotificacao
    {
         CriticidadeEnum Criticidade { get;  set; }

         string Propriedade { get;  set; }

         Guid NotificacaoId { get; }

         bool AdicionadoManualmente { get;  set; }


         string Mensagem { get;  set; }

         CamadaEnum? Camada { get;  set; }

         TipoNotificacaoEnum? TipoNotificacao { get;  set; }
    }
}
