using System;
using System.Collections.Generic;
using System.Text;

namespace WebServices.Models
{
    public class DadosWebServiceResponse<T> where T : class
    {

        public int StatusCode { get; private set; }

        public T DadosRetorno { get; private set; }

        public void SetStatusCode(int statusCode)
        {
            StatusCode = statusCode;
        }

        public DadosWebServiceResponse(T dadosRetorno, int statusCode)
        {
            DadosRetorno = dadosRetorno;
            StatusCode = statusCode;
        }

    }
}
