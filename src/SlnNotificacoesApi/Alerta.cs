using Resources.Enuns;
using SlnNotificacoesApi.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlnNotificacoesApi
{
    public class Alerta : Notificacao
    {
        public Alerta(CriticidadeEnum criticidade, string propriedade, string mensagem, CamadaEnum? camada, ResourceValueEnum? tipoNotificacaoEnum)
            : base(criticidade, propriedade, mensagem, camada, tipoNotificacaoEnum)
        {
        }
    }
}
