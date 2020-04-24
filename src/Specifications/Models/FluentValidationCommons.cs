using MetodosComunsApi;
using Resources.Models;
using SlnNotificacoesApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Specifications.Models
{
   public class FluentValidationCommons
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resources"></param>
        /// <returns></returns>
        public static Action<string, FluentValidation.Validators.CustomContext> ECpf(Resources<string> resources)
        {
            return (cpf, context) => {

                if (!cpf.ECpf())
                {
                    context.AddFailure(new ValidationFailureCustom(null, null, new Erro(resources)));
                }
            };
            
        }


        public static Action<Guid, FluentValidation.Validators.CustomContext> EGuid(Resources<string> resources)
        {
            return (guid, context) => {
                var guidResult = Guid.Empty;
                if (!Guid.TryParse(guid.ToString(), out guidResult))
                {

                    context.AddFailure(new ValidationFailureCustom(null, null, new Erro(resources)));
                }
                               
            };

        }


        public static Action<string, FluentValidation.Validators.CustomContext> ECpfUpdate(Resources<string> resources)
        {
            return (cpf, context) => {

                if ( !string.IsNullOrEmpty(cpf) && !cpf.ECpf())
                {
                    context.AddFailure(new ValidationFailureCustom(null, null, new Erro(resources)));
                }
            };

        }

        public static Action<string, FluentValidation.Validators.CustomContext> SobreNome(Resources<string> resources)
        {
            return (nome, context) =>
            {
                if (string.IsNullOrEmpty(nome) || (!string.IsNullOrEmpty(nome) && (nome.Length > 150 || nome.Length < 3)))
                {
                    context.AddFailure(new ValidationFailureCustom(null, null, new Erro(resources)));
                }
            };

        }

        public static Action<string, FluentValidation.Validators.CustomContext> SobreNomeUpdate(Resources<string> resources)
        {
            return (nome, context) =>
            {
                if (!string.IsNullOrEmpty(nome)  && (nome.Length > 150 || nome.Length < 3))
                {
                    context.AddFailure(new ValidationFailureCustom(null, null, new Erro(resources)));
                }
            };

        }

        /// <summary>
        /// data menor que 18 anos de menor
        /// </summary>
        /// <param name="resources"></param>
        /// <returns></returns>
        public static Action<DateTime, FluentValidation.Validators.CustomContext> Maior18(Resources<string> resources)
        {
            return (dataNascimento, context) =>
            {

              
                    if (!dataNascimento.DataValidaSQL())
                    {
                        context.AddFailure(new ValidationFailureCustom(null, null, new Erro(resources)));

                    }
                    else

                    {
                        var dataHj = DateTime.Now;
                        var datadif = new DiferencaEntreDatas(dataHj, dataNascimento);
                        if (datadif.Years < 18)
                            context.AddFailure(new ValidationFailureCustom(null, null, new Erro(resources)));
                    }
                

            };

        }

        public static Action<DateTime, FluentValidation.Validators.CustomContext> Maior18Update(Resources<string> resources)
        {
            return (dataNascimento, context) =>
            {

                if (dataNascimento != default(DateTime))
                {
                    if (!dataNascimento.DataValidaSQL())
                    {
                        context.AddFailure(new ValidationFailureCustom(null, null, new Erro(resources)));

                    }
                    else

                    {
                        var dataHj = DateTime.Now;
                        var datadif = new DiferencaEntreDatas(dataHj, dataNascimento);
                        if (datadif.Years < 18)
                            context.AddFailure(new ValidationFailureCustom(null, null, new Erro(resources)));
                    }
                }

            };

        }


        public static Action<DateTime, FluentValidation.Validators.CustomContext> Maior12(Resources<string> resources)
        {
            return (dataNascimento, context) =>
            {

                if (dataNascimento == default(DateTime) || dataNascimento == DateTime.MinValue)
                {
                    context.AddFailure(new ValidationFailureCustom(null, null, new Erro(resources)));
                    return;
                }
                var dataHj = DateTime.Now;
                var datadif = new DiferencaEntreDatas(dataHj, dataNascimento);
                if (datadif.Years < 12)
                    context.AddFailure(new ValidationFailureCustom(null, null, new Erro(resources)));
            };

        }

        /// <summary>
        /// validacao do campo nome basico com 150 caracteres
        /// </summary>
        /// <param name="resources"></param>
        /// <returns></returns>
        public static Action<string, FluentValidation.Validators.CustomContext> Nome(Resources<string> resources)
        {
            return (nome, context) =>
            {
                if (string.IsNullOrEmpty(nome) || (!string.IsNullOrEmpty(nome) && (nome.Length>150 || nome.Length < 3 ) ))
                {
                    context.AddFailure(new ValidationFailureCustom(null, null, new Erro(resources)));
                }
            };

        }

        public static Action<string, FluentValidation.Validators.CustomContext> NomeUpdate(Resources<string> resources)
        {
            return (nome, context) =>
            {
                if (!string.IsNullOrEmpty(nome)  && (nome.Length > 150 || nome.Length < 3))
                {
                    context.AddFailure(new ValidationFailureCustom(null, null, new Erro(resources)));
                }
            };

        }



        public static Action<string, FluentValidation.Validators.CustomContext> EmailUpdate(Resources<string> resources)
        {
            return (email, context) =>
            {
                if ((!string.IsNullOrEmpty(email) && (!email.ValidarEmail() && (email.Length < 3 || email.Length > 50))))
                {
                    context.AddFailure(new ValidationFailureCustom(null, null, new Erro(resources)));
                }
            };

        }

        public static Action<string, FluentValidation.Validators.CustomContext> Email(Resources<string> resources)
        {
            return (email, context) =>
            {
                if (string.IsNullOrEmpty(email) || (!string.IsNullOrEmpty(email) && (!email.ValidarEmail() && (email.Length < 3 || email.Length > 50)) ))
                {
                    context.AddFailure(new ValidationFailureCustom(null, null, new Erro(resources)));
                }

        
            };

        }
    }
}
