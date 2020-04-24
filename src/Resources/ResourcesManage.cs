using MetodosComunsApi;
using Microsoft.Extensions.Configuration;
using Resources.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Resources.Models;

namespace Resources
{
    public class ResourcesManage
    {


        static string principalres { get { return ResourcesManageCreate.ObterStrConModel(); } }
        private static string resdata { get { return ObterJson("resource-data").Value; } }
        private static string resvalue { get { return ObterJson("resource-value").Value; } }
        private static string rescomment { get { return ObterJson("resource-comment").Value; } }
        private static string resname { get { return ObterJson("resource-name").Value; } }
        private static string resmodulo { get { return ObterJson("resource-modulo").Value; } }
        static string consultaresources { get { return ObterJson("resource-consulta-resources").Value; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resources"></param>
        /// <returns></returns>
        public static IEnumerable<Models.Resources<string>> ObterResources(Models.Resources<string> resources,
                        IEnumerable<Models.Resources<string>> resourcesList = null)
        {
            var strBusca = principalres;

            if (consultaresources.Equals("lista"))
                return ObterResourcesLista(resources, resourcesList);


            if (consultaresources.Equals("listamemoria"))
                return ObterResourcesListaMemoria(resources, resourcesList);


            return ObterResourcesArquivos(resources, resourcesList, strBusca);

        }

        private static IEnumerable<Resources<string>> ObterResourcesListaMemoria(Resources<string> resources, IEnumerable<Resources<string>> resourcesList)
        {
            
            var query = SingletonContexto.Instance.Resources.AsQueryable();

            if (resources != null)
            {
                if (!string.IsNullOrEmpty(resources.Modulo)
                  && System.Enum.IsDefined(typeof(ModulosEnum), resources.Modulos.Value))
                    query = query.Where(x => x.Modulos == resources.Modulos);


                if (System.Enum.IsDefined(typeof(ResourceValueEnum), resources.ResourceValue))
                    query = query.Where(x => x.ResourceValue == resources.ResourceValue);
            }


            if (resourcesList != null && resourcesList.Any())
            {

                var ModulosStr = resourcesList.Where(x => x.Modulos.HasValue && System.Enum.IsDefined(typeof(ModulosEnum), x.Modulos))
                                           .Select(x => x.Modulo).Distinct();

                var Nomes = resourcesList.Where(x => System.Enum.IsDefined(typeof(ResourceValueEnum), x.ResourceValue))
                                            .Select(x => x.Name).Distinct();


                //if (ModulosStr != null && ModulosStr.Any())
                //    query = query.Where(x => ModulosStr.Contains(x.Modulos.ToString()));
                if (Nomes != null && Nomes.Any())
                    query = query.Where(x => Nomes.Contains(x.ResourceValue.ToString()));

            }

            return query.ToList();
        }

        static IEnumerable<Models.Resources<string>> ObterResourcesLista(Models.Resources<string> resources, IEnumerable<Models.Resources<string>> resourcesList)
        {
            var query = ResourcesManageCreate.ResourcesLista.AsQueryable();

            if (resources != null)
            {
                if (!string.IsNullOrEmpty(resources.Modulo)
                  && System.Enum.IsDefined(typeof(ModulosEnum), resources.Modulos.Value))
                    query = query.Where(x => x.Modulos == resources.Modulos);


                if (System.Enum.IsDefined(typeof(ResourceValueEnum), resources.ResourceValue))
                    query = query.Where(x => x.ResourceValue == resources.ResourceValue);
            }


            if (resourcesList != null && resourcesList.Any())
            {

                var ModulosStr = resourcesList.Where(x => x.Modulos.HasValue && System.Enum.IsDefined(typeof(ModulosEnum), x.Modulos))
                                           .Select(x => x.Modulo).Distinct();

                var Nomes = resourcesList.Where(x => System.Enum.IsDefined(typeof(ResourceValueEnum), x.ResourceValue))
                                            .Select(x => x.Name).Distinct();


                //if (ModulosStr != null && ModulosStr.Any())
                //    query = query.Where(x => ModulosStr.Contains(x.Modulos.ToString()));
                if (Nomes != null && Nomes.Any())
                    query = query.Where(x => Nomes.Contains(x.ResourceValue.ToString()));

            }

            return query.ToList();
        }

        static IEnumerable<Models.Resources<string>> ObterResourcesArquivos(Models.Resources<string> resources, IEnumerable<Models.Resources<string>> resourcesList, string strBusca)
        {
            var el = XElement.Load(strBusca).Descendants(resdata);
            var query = el.AsQueryable();
            var resret = new List<Models.Resources<string>>();

            var re_value = resvalue;
            var re_comment = rescomment;
            var re_name = resname;
            var re_mod = resmodulo;
            if (resources != null)
            {
                if (!string.IsNullOrEmpty(resources.Modulo)
                  && System.Enum.IsDefined(typeof(ModulosEnum), resources.Modulos.Value))
                    query = query.Where(x => x.Attribute(re_mod) != null && x.Attribute(re_mod).Value.Equals(resources.Modulo));


                if (System.Enum.IsDefined(typeof(ResourceValueEnum), resources.ResourceValue))
                    query = query.Where(x => x.Attribute(re_name).Value.Equals(resources.Name));
            }

            if (resourcesList != null && resourcesList.Any())
            {

                var Modulos = resourcesList.Where(x => x.Modulos.HasValue && System.Enum.IsDefined(typeof(ModulosEnum), x.Modulos))
                                           .Select(x => x.Modulo).Distinct();

                var Nomes = resourcesList.Where(x => System.Enum.IsDefined(typeof(ResourceValueEnum), x.ResourceValue))
                                            .Select(x => x.Name).Distinct();
                if (Modulos != null && Modulos.Any())
                    query = query.Where(x => x.Attribute(re_mod) != null && Modulos.Contains(x.Attribute(re_mod).Value));
                if (Nomes != null && Nomes.Any())
                {

                    query = query.Where(x => x.Attribute(re_name) != null && Nomes.Contains(x.Attribute(re_name).Value));

                }

            }
            var el2 = query.ToList();
            foreach (var x in el2)
            {
                resret.Add(new Models.Resources<string>
                {

                    Modulos = x.Attribute(re_mod) != null
                     ? x.Attribute(re_mod).Value.ObterEnumToName<Resources.Enuns.ModulosEnum>()
                     : (Resources.Enuns.ModulosEnum?)null,
                    ResourceValue = x.Attribute(re_name) != null ? x.Attribute(re_name).Value
                                 .ObterEnumToName<Resources.Enuns.ResourceValueEnum>() : 0,
                    Mensagem = x.Descendants(re_value) != null && x.Descendants(re_value).Any() ? x.Descendants(re_value).FirstOrDefault().Value : string.Empty,
                    Observacao = x.Descendants(re_comment) != null && x.Descendants(re_comment).Any() ? x.Descendants(re_comment).FirstOrDefault().Value : string.Empty

                });

            }

            return resret;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="resources"></param>
        public static void ExcluirResource(Models.Resources<string> resources)
        {

            if (consultaresources.Equals("lista"))
            {
                Models.Resources<string> resourcesRemove = new Models.Resources<string>();
                ResourcesManageCreate.RemoverResourcesLista(new Models.ResourceKeyValue { Modulos = resources.Modulos, ResourceValue = resources.ResourceValue }, out resourcesRemove);
                return;
            }

            var el = Obteres(resources);
            if (el != null)
            {
                el.Remove();
                el.Save(principalres);
            }

        }



        static XElement Obteres(Models.Resources<string> resources)
        {
            return XElement.Load(principalres).Descendants(resdata)
                .FirstOrDefault(ObterChaveValor(resources));
        }


        /// <summary>
        /// Função que obtém chave valor 
        /// </summary>
        /// <param name="resources"></param>
        /// <returns></returns>
        static System.Func<XElement, bool> ObterChaveValor(Models.Resources<string> resources)
        {
            return z => z.Attribute(resname) != null && z.Attribute(resname).Value.Equals(resources.Name)
                            && z.Attribute(resmodulo) != null && resources.Modulos.HasValue &&
                            z.Attribute(resmodulo).Value.Equals(resources.Modulos.Value.ToString());
        }

        public static bool ExisteResource(Models.Resources<string> resources)
        {

            if (consultaresources.Equals("lista"))
            {
                return ResourcesManageCreate.ResourcesLista.Any(x => x.ResourceValue == resources.ResourceValue && x.Modulos == resources.Modulos);
            }

            return XElement.Load(principalres).Descendants(resdata).Any(
                        ObterChaveValor(resources)
            );
        }

        public static void AdicionarResource(Resources.Models.Resources<string> resources, string dirsave = null)
        {



            if (consultaresources.Equals("lista"))
            {
                ResourcesManageCreate.AdicionarResourcesLista(
                        new KeyValuePair<Models.ResourceKeyValue, Models.Resources<string>>(
                            new Models.ResourceKeyValue
                            {
                                Modulos = resources.Modulos,
                                ResourceValue = resources.ResourceValue
                            },
                            resources));
                return;
            }



            if (consultaresources.Equals("listamemoria"))
            {
                ResourcesManageCreate.AdicionarResourcesListaMemoria(new KeyValuePair<Models.ResourceKeyValue, Models.Resources<string>>(
                            new Models.ResourceKeyValue
                            {
                                Modulos = resources.Modulos,
                                ResourceValue = resources.ResourceValue
                            },
                            resources));
                return;
            }


            var dirSaveXml = dirsave ?? principalres;
            XElement x = new XElement(resdata);

            if (resources.Modulos.HasValue)
                x.Add(new XAttribute(resmodulo, resources.Modulos.Value.ToString()));

            x.Add(new XAttribute(resname, resources.Name));

            if (!string.IsNullOrEmpty(resources.Mensagem))
            {
                var eladd = new XElement(resvalue);
                eladd.Value = resources.Mensagem;
                x.Add(eladd);
            }

            if (!string.IsNullOrEmpty(resources.Observacao))
            {
                var eladdob = new XElement(rescomment);
                eladdob.Value = resources.Observacao;
                x.Add(eladdob);
            }

            XElement xml = XElement.Load(dirSaveXml);
            xml.Add(x);
            xml.Save(dirSaveXml);


        }


        static IConfigurationSection ObterJson(string section)
        {

            string basePath = System.AppContext.BaseDirectory;
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            return configuration.GetSection(section);
        }


    }
}
