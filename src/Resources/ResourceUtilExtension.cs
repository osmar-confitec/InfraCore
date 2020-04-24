using Resources.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Resources.Util
{
   public static class ResourceUtilExtension
    {
        public static string ObterResourceMessage(this IEnumerable<Models.Resources<string>> resources, ModulosEnum? Modulos, ResourceValueEnum resourceValueEnum)
        {
            return  ObterRes(resources, Modulos, resourceValueEnum)?.Mensagem  ;
        }

        static Models.Resources<string> ObterRes(this IEnumerable<Models.Resources<string>> resources, ModulosEnum? Modulos, ResourceValueEnum resourceValueEnum)
        {

            if (Modulos.HasValue && System.Enum.IsDefined(typeof(ModulosEnum), Modulos.Value) && System.Enum.IsDefined(typeof(ResourceValueEnum), resourceValueEnum))
                return resources.FirstOrDefault(x => x.Modulos.HasValue && x.Modulos.Value.Equals(Modulos.Value) && x.ResourceValue == resourceValueEnum);

            if (System.Enum.IsDefined(typeof(ResourceValueEnum), resourceValueEnum))
                return resources.FirstOrDefault(x => x.ResourceValue == resourceValueEnum && (!x.Modulos.HasValue || !System.Enum.IsDefined(typeof(ModulosEnum), x.Modulos.Value)));

            return null;
        }

        public static Models.Resources<string> ObterResourceMessageClass(this IEnumerable<Models.Resources<string>> resources, ModulosEnum? Modulos, ResourceValueEnum resourceValueEnum)
        {
            return ObterRes( resources,  Modulos, resourceValueEnum);
        }
    }
}
