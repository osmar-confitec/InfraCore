using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTesterCons
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var res = Resources.ResourcesManage.ObterResources(null, new List<Resources.Models.Resources<string>>{
                new Resources.Models.Resources<string>{
                    ResourceValue = Resources.Enuns.ResourceValueEnum.DescricaoNomeRequerido
                    },
                new Resources.Models.Resources<string>{
                    ResourceValue = Resources.Enuns.ResourceValueEnum.SobreNomeRequerido
                    }
         });

    
            Console.WriteLine(string.Join(System.Environment.NewLine, res));
            Console.ReadKey();

        }
    }
}
