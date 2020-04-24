using MetodosComunsApi;
using SlnNotificacoesApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using WebServices.En;
using WebServices.Interfaces;

namespace WebServices.Base
{

    public enum StatusCodeHttp
    {
        [Description(" Estas requisição foi bem sucedida. O significado do sucesso varia de acordo com o método HTTP: ")]
        OK = 200,
        [Description(" A requisição foi bem sucedida e um novo recurso foi criado como resultado. Esta é uma tipica resposta enviada após uma requisição POST. ")]
        Created = 201,
        [Description("A requisição foi recebida mas nenhuma ação foi tomada sobre ela. Isto é uma requisição não-comprometedora, o que significa que não há nenhuma maneira no HTTP para enviar uma resposta assíncrona indicando o resultado do processamento da solicitação. Isto é indicado para casos onde outro processo ou servidor lida com a requisição, ou para processamento em lote.")]
        Accepted = 202,
        [Description("Esse código de resposta significa que o conjunto de meta-informações retornadas não é o conjunto exato disponível no servidor de origem, mas coletado de uma cópia local ou de terceiros. Exceto essa condição, a resposta de 200 OK deve ser preferida em vez dessa resposta.")]
        Non_Authoritative_Information = 203,
        [Description("Não há conteúdo para enviar para esta solicitação, mas os cabeçalhos podem ser úteis. O user-agent pode atualizar seus cabeçalhos em cache para este recurso com os novos.")]
        No_Content = 204,
        [Description("Essa resposta significa que o servidor não entendeu a requisição pois está com uma sintaxe inválida")]
        Bad_Request = 400,
        [Description("Embora o padrão HTTP especifique , semanticamente, essa resposta significa . Ou seja, o cliente deve se autenticar para obter a resposta solicitada.")]
        Unauthorized = 401,
        [Description("O servidor não pode encontrar o recurso solicitado. Este código de resposta talvez seja o mais famoso devido à frequência com que acontece na web")]
        Not_Found = 404


    }

    public abstract class WebServicesBase : IServiceBase
    {


        protected StatusCodeHttp StatusCodeHttp { get { return (StatusCodeHttp)StatusCode; } }

        public int StatusCode { get; protected set; } = 0;

        protected bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {

                // Free any other managed objects here.
                //
            }

            disposed = true;
        }

        protected string UrlWs { get; private set; }




        protected IDictionary<EMediaType, string> Headers;
        public IReadOnlyDictionary<EMediaType, string> HeadersGet => Headers.ToDictionary(kv => kv.Key, kv => kv.Value);

        protected string HeaderToken { get; set; } = "Bearer Token";
        public List<Notificacao> notificacoes { get; set; }

        protected WebServicesBase()
        {
            notificacoes = new List<Notificacao>();
            Headers = new Dictionary<EMediaType, string>();
            UrlWs = ConfigurationManager.AppSettings["UrlServicoBase"];
            PopularHeaders();
        }

        protected virtual void PopularHeaders()
        {
            var loop = System.Enum.GetValues(typeof(EMediaType)).Cast<EMediaType>();
            foreach (var item in loop)
                Headers.Add(item, item.ObterAtributoDescricao());
        }
    }
}
