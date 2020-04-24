using MetodosComunsApi;
using Resources.Enuns;
using SlnNotificacoesApi.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlnNotificacoesApi
{
    public abstract class Notificacao
    {

        protected Notificacao()
        {
            TipoNoty =   this.GetType().Name.ObterEnumToName<TipoNotyEnum>();
            NotificacaoId = Guid.NewGuid();
        }


        protected Notificacao(CriticidadeEnum criticidade, string propriedade, string mensagem, CamadaEnum? camada, ResourceValueEnum? tiponotificacao):this()
        {
            Criticidade = criticidade;
            Propriedade = propriedade;
            Mensagem = mensagem;
            
            Camada = camada;
            TipoNotificacao = tiponotificacao;
          
        }

        protected Notificacao(Resources.Models.Resources<string> resources)
            :this(  CriticidadeEnum.Media, string.Empty, resources.ResourceValue.ObterAtributoDescricao(),null, resources.ResourceValue)
        {

            Resources = resources;
            Mensagem = Resources.Mensagem;

        }

        public CriticidadeEnum Criticidade { get; private set; }

        public TipoNotyEnum TipoNoty { get; private set; }

        public string Propriedade { get; private set; }

        protected Guid NotificacaoId { get; }

        public bool AdicionadoManualmente { get; private set; }

        public void SetarAdicionadoManualmente(bool set)
        {
            AdicionadoManualmente = set;
        }


        public string Mensagem { get; private set; }

        protected CamadaEnum? Camada { get; private set; }

        protected Resources.Models.Resources<string> Resources { get; private set; }

        public ResourceValueEnum? TipoNotificacao { get; private set; }

        public void SetTipoNotificacao(ResourceValueEnum tipoNotificacao)
        {
            if (TipoNotificacao.HasValue && TipoNotificacao == tipoNotificacao)
                return;
            TipoNotificacao = tipoNotificacao;
        }

        public void SetMessage(string msg)
        {
            Mensagem = msg;
        }


        public void SetPropriedade(string msg)
        {
            Propriedade = msg;
        }


    }
}
