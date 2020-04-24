using Resources.Enuns;
using SlnNotificacoesApi.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlnNotificacoesApi
{
    public class Sucesso : Notificacao
    {
        public Sucesso(CriticidadeEnum criticidade, string propriedade, string mensagem, CamadaEnum? camada, ResourceValueEnum? tipoNotificacaoEnum)
            : base(criticidade, propriedade, mensagem, camada, tipoNotificacaoEnum)
        {
        }
    }
}
