using System;
using System.Collections.Generic;
using System.Text;

namespace MetodosComunsApi
{
   public class ModeloIndice<T>
    {

        public T Modelo { get; set; }

        public int Indice { get; set; }

        public ModeloIndice(T _Modelo)
        {
            if (_Modelo == null)
            {
                Modelo = (T)Activator.CreateInstance(Modelo.GetType());
            }

            Modelo = _Modelo;

        }

    }
}
