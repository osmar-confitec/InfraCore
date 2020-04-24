using System;
using System.Collections.Generic;
using System.Text;

namespace MetodosComunsApi
{
   public class ListPaginada<T>:List<T>
    {

        public int TotalRegistros { get; set; }

        public ListPaginada()
        {

        }

        public ListPaginada(IEnumerable<T> itens)
        {
            this.AddRange(itens);
        }

    }
}
