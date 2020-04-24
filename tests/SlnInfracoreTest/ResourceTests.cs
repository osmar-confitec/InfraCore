using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Resources.Data.Context;
using Resources.Models;
using Resources;

namespace SlnInfracoreTest
{
    public class ResourceTests
    {

        [Fact]
        public void TestMemory()
        {
            var options = new DbContextOptionsBuilder<Contexto>()
                       .UseInMemoryDatabase(databaseName: "Test")
                       .Options;

            using (var context = new Contexto(options))
            {
                var customer = new Resources<string>
                {
                    Mensagem = "Novo Resource",
                    Modulos = Resources.Enuns.ModulosEnum.Clientes,
                    ResourceValue = Resources.Enuns.ResourceValueEnum.ClienteExistente,
                    TipoResources = Resources.Enuns.TipoResourcesEnum.Texto

                };

                context.Resources.Add(customer);
                context.SaveChanges();

                var lista = context.Resources.ToList();
            }


        }

        [Fact]
        public void ResourceMemory()
        {
            var resMemory = new ResourcesManageMemory();

            resMemory.ObterResources(null, new List<Resources.Models.Resources<string>>{
                new Resources.Models.Resources<string>{
                    ResourceValue = Resources.Enuns.ResourceValueEnum.DescricaoNomeRequerido
                    },
                new Resources.Models.Resources<string>{
                    ResourceValue = Resources.Enuns.ResourceValueEnum.SobreNomeRequerido
                    }
         });


        }

        [Fact]
        public void Test1()
        {
            var res = Resources.ResourcesManage.ObterResources(null, new List<Resources.Models.Resources<string>>{
                new Resources.Models.Resources<string>{
                    ResourceValue = Resources.Enuns.ResourceValueEnum.DescricaoNomeRequerido
                    },
                new Resources.Models.Resources<string>{
                    ResourceValue = Resources.Enuns.ResourceValueEnum.SobreNomeRequerido
                    }
         });


            var res3 = res.FirstOrDefault();
        }

    }
}
