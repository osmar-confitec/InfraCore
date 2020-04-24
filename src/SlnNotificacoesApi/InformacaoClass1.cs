using Resources.Enuns;
using SlnNotificacoesApi.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlnNotificacoesApi
{
    public class Informacao : Notificacao
    {
        public Informacao(CriticidadeEnum criticidade,
            string propriedade, string mensagem, CamadaEnum? camada, ResourceValueEnum? tipoNotificacaoEnum)
            : base(criticidade, propriedade, mensagem, camada, tipoNotificacaoEnum)
        {
        }
    }
}
