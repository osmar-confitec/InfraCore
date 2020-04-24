using MetodosComunsApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebServices.En;
using WebServices.Models;

namespace WebServices.Base
{
    public abstract class WebServiceREST : WebServicesBase
    {


        protected string RetornoBody { get; private set; }



        protected IDictionary<string, string> Token { get; set; }

        protected WebServiceREST()
        {
            Token = new Dictionary<string, string>();
        }

        protected virtual void ObterResponseAsync(string body, int statusCode)
        {
            StatusCode = statusCode;
            RetornoBody = body;
        }

        #region " Verbos "

        protected virtual async Task<DadosWebServiceResponse<T>> Delete<T>(string page)
                                                   where T : class
        {
            /*Requests baseados em json*/
            DadosWebServiceResponse<T> ret = new DadosWebServiceResponse<T>(null, 0);
            using (var client = new HttpClient())
            {

                BaseHeader(EMediaType.Requestjson, client);
                HttpResponseMessage response = await client.DeleteAsync(page);
                string retornoBody = await AtualizarBodyStatudCode(ret, response);

                if (response.IsSuccessStatusCode)
                {
                    ret = new DadosWebServiceResponse<T>(statusCode: (int)response.StatusCode,
                                                         dadosRetorno: JsonConvert.DeserializeObject<T>(retornoBody));
                }
            }
            return ret;

        }


        protected virtual async Task<DadosWebServiceResponse<T>> Path<T, TEnv>(string page, TEnv Env)
                                            where T : class where TEnv : class
        {

            /*Requests baseados em json*/
            DadosWebServiceResponse<T> ret = new DadosWebServiceResponse<T>(null, 0);
            using (var client = new HttpClient())
            {

                var method = new HttpMethod("PATCH");
                var jsonContent = JsonConvert.SerializeObject(Env);
                BaseHeader(EMediaType.Requestjson, client);

                var contentString = new StringContent(jsonContent, Encoding.UTF8, EMediaType.Requestjson.ObterAtributoDescricao());
                contentString.Headers.ContentType = new
                MediaTypeHeaderValue(EMediaType.Requestjson.ObterAtributoDescricao());

                var request = new HttpRequestMessage(method, page)
                {
                    Content = contentString
                };

                HttpResponseMessage response = await client.SendAsync(request);
                string retornoBody = await AtualizarBodyStatudCode(ret, response);

                if (response.IsSuccessStatusCode)
                {
                    ret = new DadosWebServiceResponse<T>(statusCode: (int)response.StatusCode,
                                                         dadosRetorno: JsonConvert.DeserializeObject<T>(retornoBody));
                }
            }
            return ret;
        }

        protected virtual async Task<DadosWebServiceResponse<T>> Post<T, TEnv>(string page, TEnv Env)
                                            where T : class where TEnv : class
        {

            /*Requests baseados em json*/
            DadosWebServiceResponse<T> ret = new DadosWebServiceResponse<T>(null, 0);
            using (var client = new HttpClient())
            {
                var jsonContent = JsonConvert.SerializeObject(Env);
                BaseHeader(EMediaType.Requestjson, client);

                var contentString = new StringContent(jsonContent, Encoding.UTF8, EMediaType.Requestjson.ObterAtributoDescricao());
                contentString.Headers.ContentType = new
                MediaTypeHeaderValue(EMediaType.Requestjson.ObterAtributoDescricao());


                HttpResponseMessage response = await client.PostAsync(page, contentString);
                string retornoBody = await AtualizarBodyStatudCode(ret, response);

                if (response.IsSuccessStatusCode)
                {
                    ret = new DadosWebServiceResponse<T>(statusCode: (int)response.StatusCode,
                                                         dadosRetorno: JsonConvert.DeserializeObject<T>(retornoBody));
                }
            }
            return ret;
        }


        protected virtual async Task<DadosWebServiceResponse<T>> Get<T>(string page) where T : class
        {
            DadosWebServiceResponse<T> ret = new DadosWebServiceResponse<T>(null, 0);
            using (var client = new HttpClient())
            {
                BaseHeader(EMediaType.Requestjson, client);

                HttpResponseMessage response = await client.GetAsync(page);
                string retornoBody = await AtualizarBodyStatudCode(ret, response);

                if (response.IsSuccessStatusCode)
                {
                    ret = new DadosWebServiceResponse<T>(statusCode: (int)response.StatusCode,
                                                         dadosRetorno: JsonConvert.DeserializeObject<T>(retornoBody));
                }
            }
            return ret;
        }

        #endregion





        async Task<string> AtualizarBodyStatudCode<T>(DadosWebServiceResponse<T> ret, HttpResponseMessage response) where T : class
        {
            ret.SetStatusCode((int)response.StatusCode);
            string retornoBody = await response.Content.ReadAsStringAsync();
            ObterResponseAsync(retornoBody, (int)response.StatusCode);
            return retornoBody;
        }



        protected virtual void BaseHeader(EMediaType eMediaType, HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            if (Token.Any())
                client.DefaultRequestHeaders.Authorization =
                      new AuthenticationHeaderValue(Token.FirstOrDefault().Key, Token.FirstOrDefault().Value);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(eMediaType.ObterAtributoDescricao()));
            client.BaseAddress = new Uri(UrlWs);
        }
    }
}
