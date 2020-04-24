using MetodosComunsApi;
using Microsoft.EntityFrameworkCore;
using Resources.Data.Context;
using Resources.Enuns;
using Resources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Resources
{

    public sealed class SingletonContexto
    {
        SingletonContexto()
        {
        }
        private static readonly object padlock = new object();
        private static Contexto instance = null;
        public static Contexto Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        var options = new DbContextOptionsBuilder<Contexto>()
                              .UseInMemoryDatabase(databaseName: "DataBaseMemoriaResource")
                              .Options;
                        instance = new Contexto(options);
                    }
                    return instance;
                }
            }
            set {

                instance = value;
            }
        }
    }

    public class ResourcesManageMemory
    {

        IDictionary<LinguagemArquivosEnum, List<Models.Resources<string>>> _Arquivos;

        const string settings = "appsettings.json";

        public bool RePopularLinguagens { get; set; }

        string linguagemescolhida { get { return MetodosComunsApi.MetodosComuns.ObterJson("resource-linguagem-escolhida",settings ).Value; } }

        void PopularLinguagem()
        {
            _Arquivos = new Dictionary<LinguagemArquivosEnum, List<Models.Resources<string>>>();
            var all = SingletonContexto.Instance.Resources.ToList();
            SingletonContexto.Instance.Resources.RemoveRange(all);
            GerarListaPortugues();
            GerarListaUs();
            var itensiterar = _Arquivos.Where(x => x.Key.Equals(linguagemescolhida.ObterEnumToName<LinguagemArquivosEnum>()));

            foreach (var item in itensiterar)
            {
                TratarEnumsResource(item.Value);
                foreach (var itens in item.Value)
                    AdicionarResource(itens);
            }

        }

        public IEnumerable<Resources<string>> ObterResources(Resources<string> resources, IEnumerable<Resources<string>> resourcesList)
        {
            if (RePopularLinguagens)
            {
                PopularLinguagem();
                RePopularLinguagens = false;
            }
               

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


                if (Nomes != null && Nomes.Any())
                    query = query.Where(x => Nomes.Contains(x.ResourceValue.ToString()));

            }

            return query.ToList();
        }

        void AdicionarResource(Models.Resources<string> resource)
        {

            if (SingletonContexto.Instance.Resources.Any(x => x.Modulos == resource.Modulos && x.ResourceValue == resource.ResourceValue))
            {
                var resFind = SingletonContexto.Instance.Resources.FirstOrDefault(x => x.Modulos == resource.Modulos && x.ResourceValue == resource.ResourceValue);
                resFind.Mensagem = resource.Mensagem;
                resFind.Modulos = resource.Modulos;
                resFind.Observacao = resource.Observacao;
                resFind.ResourceValue = resource.ResourceValue;
                resFind.TipoResources = resource.TipoResources;
                SingletonContexto.Instance.SaveChanges();
                return;
            }
            SingletonContexto.Instance.Resources.Add(resource);
            SingletonContexto.Instance.SaveChanges();

        }

        void TratarEnumsResource(IEnumerable<Models.Resources<string>> resources)
        {
            foreach (var itemenu in System.Enum.GetNames(typeof(ResourceValueEnum)))
            {
                var resv = itemenu.ObterEnumToName<ResourceValueEnum>();
                foreach (var itemmod in System.Enum.GetNames(typeof(ModulosEnum)))
                {
                    var mod = itemmod.ObterEnumToName<ModulosEnum>();
                    if (resources.Any(x => x.ResourceValue.Equals(resv) && x.Modulos.Equals(mod) ))
                        continue;

                    AdicionarResource(new Models.Resources<string>()
                    {
                        Modulos = mod,
                        ResourceValue = resv,
                        Mensagem = "NDA"
                    });
                }
                if (resources.Any(x => x.ResourceValue.Equals(resv) && !x.Modulos.HasValue))
                    continue;
                AdicionarResource(new Models.Resources<string>()
                {
                    ResourceValue = resv,
                    Mensagem = "NDA"
                });

            }
        }


        #region " Geração da Linguagens "

        void GerarListaUs()
        {
            var Us = LinguagemArquivosEnum.UsEng;

            _Arquivos.Add(Us, new List<Models.Resources<string>>(){

                 new Models.Resources<string>(){
                        
                        Mensagem = "Attention CPF {0} invalid.",
                        ResourceValue = ResourceValueEnum.CPFInvalido
                    },

                 new Models.Resources<string>(){

                        Mensagem = "User attention must be older than twelve years",
                        ResourceValue = ResourceValueEnum.Maiorde12
                    },

                    new Models.Resources<string>(){



                    }
                });
        }

        void GerarListaPortugues()
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
                        Modulos = ModulosEnum.Usuario,
                        Mensagem = "Atenção confirmação de senha não válida!",
                        ResourceValue = ResourceValueEnum.UsuarioSenhaConfirmNaoBatem
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

        #endregion

        public ResourcesManageMemory()
        {
            PopularLinguagem();
        }

    }
}
