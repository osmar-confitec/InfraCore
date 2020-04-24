using Resources.Enuns;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.Models
{
   public class Resources<T>
    {
        
        public Guid Id { get; set; }

        public T Mensagem { get; set; }

        public string Observacao { get; set; }

        public ModulosEnum? Modulos { get; set; }

        public string Name { get { return ResourceValue.ToString();  } }

        public string Modulo { get { return Modulos.HasValue ? Modulos.ToString() : string.Empty ; } }

        public TipoResourcesEnum TipoResources { get; set; }

        public ResourceValueEnum ResourceValue { get; set; }

        public Resources()
        {
            TipoResources = TipoResourcesEnum.Texto;
            Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return $"nome:{Name} - modulo:{Modulo} mensagem:{Mensagem}";
        }

    }
}
