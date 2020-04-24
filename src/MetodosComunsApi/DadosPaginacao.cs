using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetodosComunsApi
{
   public class DadosPaginacao<T> where T:class
    {

        public T DataPaginacao { get;  set; }

        public int Pagina { get;  set; }

        public string Ordenacao { get;  set; }

        public bool Direcao { get;  set; }

        public int QuantidadeRegistrosPagina { get;   set; }

        public DadosPaginacao()
        {
            DataPaginacao = (T)Activator.CreateInstance(typeof(T));
        }


        public void SetarDataPaginacao(T data)
        {
            DataPaginacao = data;
        }



   
    }
}
