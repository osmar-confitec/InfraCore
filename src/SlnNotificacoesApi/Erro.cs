using Resources.Enuns;
using SlnNotificacoesApi.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlnNotificacoesApi
{
    public class Erro : Notificacao
    {
        public Erro(CriticidadeEnum criticidade, string propriedade, string mensagem, CamadaEnum? camada, ResourceValueEnum? tipoNotificacaoEnum)
            : base(criticidade, propriedade, mensagem, camada, tipoNotificacaoEnum)
        {
        }

        public Erro(Resources.Models.Resources<string> resources):base(resources)
        {

        }
    }
}
