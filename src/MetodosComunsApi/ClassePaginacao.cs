using System;
using System.Collections.Generic;
using System.Text;

namespace MetodosComunsApi
{
   public class ClassePaginacao<T>
    {

        public List<T> Lista { get; set; }

        public int TotalRegistros { get; set; }

        public int TotalPaginas { get { return Convert.ToInt32(Math.Ceiling((Convert.ToDecimal(TotalRegistros) / Convert.ToDecimal(ItensPorPagina)))); } }
        
        public int ItensPorPagina { get; set; }

        public ClassePaginacao()
        {
            Lista = new List<T>();
            ItensPorPagina = 10;
        }

        public ClassePaginacao(IEnumerable<T> itens):this()
        {
            Lista.AddRange(itens);
        }


    }
}
