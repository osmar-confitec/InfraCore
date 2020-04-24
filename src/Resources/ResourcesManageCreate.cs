using MetodosComunsApi;
using Microsoft.Extensions.Configuration;
using Resources.Enuns;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Concurrent;
using Resources.Models;
using Resources.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Resources
{



    public class ResourcesManageCreate
    {
        static IDictionary<LinguagemArquivosEnum, List<Models.Resources<string>>> _Arquivos;

        static string pastaprincipal { get { return ObterJson("resource-pasta-principal").Value; } }
        static string consultaresources { get { return ObterJson("resource-consulta-resources").Value; } }
        static string linguagemescolhida { get { return ObterJson("resource-linguagem-escolhida").Value; } }
        static string arquivoleitura { get { return ObterJson("resource-arquivo-leitura").Value; } }
        static string template { get { return ObterJson("resource-template-leitura").Value; } }
        static string resname { get { return ObterJson("resource-name").Value; } }
        static string resrecalcular { get { return ObterJson("resource-recalcular").Value; } }
        static string resvalue { get { return ObterJson("resource-value").Value; } }
        static string resdata { get { return ObterJson("resource-data").Value; } }
        //static string reskeyRecalcular { get { return ObterJson("resource-key-recalcular").Value; } }


       

        static ConcurrentDictionary<ResourceKeyValue, Resources.Models.Resources<string>> _ListResources;
        static ConcurrentDictionary<ResourceKeyValue, Resources.Models.Resources<string>> ListResources
        {
            get
            {
                if (_ListResources == null)
                    _ListResources = new ConcurrentDictionary<ResourceKeyValue, Resources<string>>();
                return _ListResources;
            }
            set
            {
                _ListResources = value;
            }
        }

        public static IEnumerable<Resources.Models.Resources<string>> ResourcesLista { get { return ListResources.Select(x => x.Value); } }


        public static bool RemoverResourcesLista(ResourceKeyValue key, out Resources.Models.Resources<string> resadd)
        {
            return ListResources.TryRemove(key, out resadd);

        }

        public static bool AtualizarResourcesLista(KeyValuePair<ResourceKeyValue, Resources.Models.Resources<string>> resadd)
        {
            if (ListResources.Any(x => x.Key == resadd.Key))
            {
                var val = ListResources.FirstOrDefault(x => x.Key == resadd.Key).Value;
                return ListResources.TryUpdate(resadd.Key, resadd.Value, val);
            }

            return false;
        }

        public static bool AdicionarResourcesLista(KeyValuePair<ResourceKeyValue, Resources.Models.Resources<string>> resadd)
        {


            if (!ListResources.Any(x => x.Key == resadd.Key))
                return ListResources.TryAdd(resadd.Key, resadd.Value);
            else
               return AtualizarResourcesLista(new KeyValuePair<ResourceKeyValue, Resources<string>>(resadd.Key, resadd.Value));
              

        }


        public ResourcesManageCreate()
        {
            // GerarArquivos();
        }

        private static void GerarArquivos()
        {
            _Arquivos = new Dictionary<LinguagemArquivosEnum, List<Models.Resources<string>>>();

            GerarListaPortugues();
            GerarListaUs();

            if (consultaresources.Equals("lista"))
                GerarArquivosResourcesLista();
            else
                GerarArquivosNaPasta();
        }

        static void GerarArquivosResourcesLista()
        {

            //linguagemescolhida
            var itensiterar = _Arquivos.Where(x => x.Key.Equals(linguagemescolhida.ObterEnumToName<LinguagemArquivosEnum>()));
            foreach (var item in itensiterar)
            {
                TratarEnumsResource(item, null);
                foreach (var itens in item.Value)
                    AdicionarResourcesLista(new KeyValuePair<ResourceKeyValue, Resources<string>>(new ResourceKeyValue { Modulos = itens.Modulos, ResourceValue = itens.ResourceValue }, itens));


            }


        }

        static void GerarArquivosNaPasta()
        {


            string dirPasta = GerarPastaPrincipal();

            /*verificar template base*/
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(GerarTemplateBase());

            string temp = xmlDoc.OuterXml;

            foreach (KeyValuePair<LinguagemArquivosEnum, List<Models.Resources<string>>> item in _Arquivos)
            {
                var dirPastaLinguagem = Path.Combine(dirPasta, item.Key.ObterAtributoDescricao());

                /*se basear enum*/

                if (File.Exists(dirPastaLinguagem))
                    continue;

                File.AppendAllText(dirPastaLinguagem, temp);
                TratarEnumsResource(item, dirPastaLinguagem);

                foreach (var itens in item.Value)
                    ResourcesManage.AdicionarResource(itens, dirPastaLinguagem);


            }
        }

        static void TratarEnumsResource(KeyValuePair<LinguagemArquivosEnum, List<Models.Resources<string>>> item, string dir)
        {
            foreach (var itemenu in System.Enum.GetNames(typeof(ResourceValueEnum)))
            {

                var resv = itemenu.ObterEnumToName<ResourceValueEnum>();
                if (item.Value.Any(x => x.ResourceValue.Equals(resv)))
                    continue;

                foreach (var itemmod in System.Enum.GetNames(typeof(ModulosEnum)))
                {
                    var mod = itemmod.ObterEnumToName<ModulosEnum>();
                    ResourcesManage.AdicionarResource(new Models.Resources<string>()
                    {
                        Modulos = mod,
                        ResourceValue = resv,
                        Mensagem = "NDA"
                    }, dir);
                }
                ResourcesManage.AdicionarResource(new Models.Resources<string>()
                {
                    ResourceValue = resv,
                    Mensagem = "NDA"
                }, dir);

            }
        }


        static string GerarTemplateBase()
        {
            var dirApp = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            var pp = pastaprincipal;
            var dirPasta = Path.Combine(dirApp, pastaprincipal);
            var diraquivotemplate = Path.Combine(dirPasta, template);
            if (File.Exists(diraquivotemplate))
                return diraquivotemplate;

            XDocument doc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("root")
                );
            var x = doc.Descendants("root").FirstOrDefault();

            XElement xDta = new XElement(resdata);
            xDta.Add(new XAttribute(resname, resrecalcular));

            var eladd = new XElement(resvalue);
            eladd.Value = "false";

            xDta.Add(eladd);
            x.Add(xDta);


            doc.Save(diraquivotemplate);
            return diraquivotemplate;
        }
        static string ArquivosLeitura()
        {
            var pasta = GerarPastaPrincipal();
            var arquivoleiturapath = Path.Combine(pasta, arquivoleitura);
            if (File.Exists(arquivoleiturapath))
            {
                return arquivoleiturapath;
            }

            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string ObterStrConModel()
        {


            if (consultaresources == "lista")
            {
                GerarArquivos();
                return null;
            }

            if (consultaresources == "listamemoria")
            {
                GerarArquivosMemoria();
                return null;
            }



            Func<string> RetornoArquivos = () =>
            {
                GerarArquivos();
                return ArquivosLeitura();
            };



            return ArquivosLeitura() ?? RetornoArquivos();

        }

        static void GerarArquivosMemoria()
        {
            _Arquivos = new Dictionary<LinguagemArquivosEnum, List<Models.Resources<string>>>();

            GerarListaPortugues();
            GerarListaUs();
            //linguagemescolhida
            var itensiterar = _Arquivos.Where(x => x.Key.Equals(linguagemescolhida.ObterEnumToName<LinguagemArquivosEnum>()));
            foreach (var item in itensiterar)
            {
                TratarEnumsResource(item, null);
                foreach (var itens in item.Value)
                    AdicionarResourcesListaMemoria(new KeyValuePair<ResourceKeyValue, Resources<string>>(new ResourceKeyValue { Modulos = itens.Modulos, ResourceValue = itens.ResourceValue }, itens));


            }

        }

        public static void AdicionarResourcesListaMemoria(KeyValuePair<ResourceKeyValue, Resources<string>> keyValuePair)
        {

            if (SingletonContexto.Instance.Resources.Any(x => x.Modulos == keyValuePair.Value.Modulos && x.ResourceValue == keyValuePair.Value.ResourceValue))
            {
                var resFind =  SingletonContexto.Instance.Resources.FirstOrDefault(x => x.Modulos == keyValuePair.Value.Modulos && x.ResourceValue == keyValuePair.Value.ResourceValue);
                resFind.Mensagem = keyValuePair.Value.Mensagem;
                SingletonContexto.Instance.SaveChanges();
                return;
            }
            SingletonContexto.Instance.Resources.Add(keyValuePair.Value);

            SingletonContexto.Instance.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string GerarPastaPrincipal()
        {
            var dirApp = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            var app = pastaprincipal;
            var dirPasta = Path.Combine(dirApp, pastaprincipal);
            if (!Directory.Exists(dirPasta))
                Directory.CreateDirectory(dirPasta);
            return dirPasta;
        }

        public void ReGerarArquivosLeitura()
        {


        }

        static void GerarListaUs()
        {
            var Us = LinguagemArquivosEnum.UsEng;

            _Arquivos.Add(Us, new List<Models.Resources<string>>(){

                    new Models.Resources<string>(){



                    }
                });


        }

        static void GerarListaPortugues()
        {



            var list = new List<Models.Resources<string>>(){

                    new Models.Resources<string>(){

                        Mensagem = "Houve algum problema!",
                        ResourceValue = ResourceValueEnum.Falha
                    },
                    new Models.Resources<string>(){

                        Mensagem = "Atenção de menor!",
                        ResourceValue = ResourceValueEnum.Maiorde18
                    },
                      new Models.Resources<string>(){

                        Mensagem = "Email inválido!",
                        ResourceValue = ResourceValueEnum.EmailInvalido
                    },
                       new Models.Resources<string>(){
                        Modulos  = ModulosEnum.Clientes,
                        Mensagem = "Atenção Id Inválido.",
                        ResourceValue = ResourceValueEnum.GuidInvalido
                    },
                        new Models.Resources<string>(){
                        Modulos  = ModulosEnum.Clientes,
                        Mensagem = "Atenção CPF {0} não existe.",
                        ResourceValue = ResourceValueEnum.ClienteInexistente
                    },
                    new Models.Resources<string>(){

                        Mensagem = "O nome é obrigatório!",
                        ResourceValue = ResourceValueEnum.DescricaoNomeRequerido
                    },
                     new Models.Resources<string>(){

                        Mensagem = "O CPF é inválido!",
                        ResourceValue = ResourceValueEnum.CPFInvalido
                    },
                   new Models.Resources<string>(){

                        Mensagem = "O nome deve conter entre 2 e 150 caracteres!",
                        ResourceValue = ResourceValueEnum.TamanhoCampoNome
                    },
                    new Models.Resources<string>(){

                        Mensagem = "Sobrenome é obrigatório!",
                        ResourceValue = ResourceValueEnum.SobreNomeRequerido

                    },
                     new Models.Resources<string>(){

                        Mensagem = "Atenção! senha não corresponde a uma senha segura deve conter maiusculas,minusculas e caracteres especiais e alfanuméricos! ",
                        ResourceValue = ResourceValueEnum.UsuarioSenhaInvalida,
                        Modulos = ModulosEnum.Usuario

                    },
                        new Models.Resources<string>(){

                        Mensagem = " Força da senha {0} não segura! ",
                        ResourceValue = ResourceValueEnum.UsuarioForcaSenhaInvalida,
                        Modulos = ModulosEnum.Usuario

                    },
                    new Models.Resources<string>(){

                        Mensagem = " Usuário existente para esse CPF {0} {1} ",
                        ResourceValue = ResourceValueEnum.UsuarioCPFExistente,
                        Modulos = ModulosEnum.Usuario

                    },
                    new Models.Resources<string>(){

                        Mensagem = " Email existente no cadastro de Usuários para o e-mail {0} {1} ",
                        ResourceValue = ResourceValueEnum.UsuarioEmailExistente,
                        Modulos = ModulosEnum.Usuario

                    },
                    new Models.Resources<string>(){

                        Mensagem = " Usuário {0} bloqueado por  tentativas inválidas.  ",
                        ResourceValue = ResourceValueEnum.UsuarioBloqueadoTentativasInvalidas,
                        Modulos = ModulosEnum.Usuario

                    },
                     new Models.Resources<string>(){

                        Mensagem = " Usuário ou senha incorretos.  ",
                        ResourceValue = ResourceValueEnum.UsuarioSenhaIncorretos,
                        Modulos = ModulosEnum.Usuario

                    },
                       new Models.Resources<string>(){

                        Mensagem = " Atenção para atualizar informe o Id ou CPF do Usuário.  ",
                        ResourceValue = ResourceValueEnum.UsuarioCPFIdVazio,
                        Modulos = ModulosEnum.Usuario

                    },
                        new Models.Resources<string>(){

                        Mensagem = " Atenção CPF não encontrado para o usuario {0}  ",
                        ResourceValue = ResourceValueEnum.UsuarioCPFNaoEncontrado,
                        Modulos = ModulosEnum.Usuario

                    },
                           new Models.Resources<string>(){

                        Mensagem = " Atenção Id não encontrado para o usuario {0}  ",
                        ResourceValue = ResourceValueEnum.UsuarioIdNaoEncontrado,
                        Modulos = ModulosEnum.Usuario

                    },
                             new Models.Resources<string>(){

                        Mensagem = " Atenção usuario já existente para o CPF {0} ou Email {1}  ",
                        ResourceValue = ResourceValueEnum.UsuarioExistenteCPFEmail,
                        Modulos = ModulosEnum.Usuario

                    },
                     new Models.Resources<string>(){
                        Mensagem = " Atenção usuario inativado  ",
                        ResourceValue = ResourceValueEnum.UsuarioInativo,
                        Modulos = ModulosEnum.Usuario

                    },
                     new Models.Resources<string>(){
                        Mensagem = " Atenção usuario não encontrado!  ",
                        ResourceValue = ResourceValueEnum.UsuarioIdNaoEncontrado,
                        Modulos = ModulosEnum.Usuario

                    },
                       new Models.Resources<string>(){
                        Mensagem = " Atenção módulo não encontrado!  ",
                        ResourceValue = ResourceValueEnum.UsuarioModuloInvalido,
                        Modulos = ModulosEnum.Usuario

                    },
                         new Models.Resources<string>(){
                        Mensagem = " Atenção ação não encontrada!  ",
                        ResourceValue = ResourceValueEnum.UsuarioAcaoInvalido,
                        Modulos = ModulosEnum.Usuario

                    },
                             new Models.Resources<string>(){
                        Mensagem = " Atenção ação {0} do modulo {1} existente!  ",
                        ResourceValue = ResourceValueEnum.UsuarioModuloExistente,
                        Modulos = ModulosEnum.Usuario

                    },
                             new Models.Resources<string>(){
                        Mensagem = " Atenção usuário não encontrado!  ",
                        ResourceValue = ResourceValueEnum.UsuarioNaoEncontrado,
                        Modulos = ModulosEnum.Usuario

                    }

                             //UsuarioNaoEncontrado
                             //UsuarioInativo
                };

            var ptbr = LinguagemArquivosEnum.PtBr;

            _Arquivos.Add(ptbr, list);
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
