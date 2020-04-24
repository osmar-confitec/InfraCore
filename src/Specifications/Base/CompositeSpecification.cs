using FluentValidation;
using Resources.Models;
using SlnNotificacoesApi;
using SlnNotificacoesApi.Enum;
using Specifications.Interfaces;
using Specifications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specifications.Base
{
    public abstract class CompositeSpecification<T> : AbstractValidator<T>, ISpecification<T>
    {

        List<Resources.Models.Resources<string>> _Res;
        public IReadOnlyList<Resources.Models.Resources<string>> Res { get { return _Res.AsReadOnly(); } }

        public virtual async Task<bool> IsSatisfiedBy(T candidate)
        {

            return await Validar(candidate);
        }

        public void AdicionarListaResources(IEnumerable<Resources.Models.Resources<string>> Res)
        {
            foreach (var item in Res)
            {
                if (!_Res.Any(x => x.Modulos == item.Modulos && item.ResourceValue == x.ResourceValue))
                    _Res.Add(item);
            }

        }

        ListNotificacoes<Notificacao> _Notificacoes;
        public IReadOnlyList<Notificacao> Notificacoes { get { return _Notificacoes.AsReadOnly(); } }

        public virtual async Task<bool> Validar(T ObjValidar)
        {
            var Result = await ValidateAsync(ObjValidar);
            foreach (var failure in Result.Errors)
            {
                if (failure is ValidationFailureCustom)
                {
                    /*obter enum pelo name*/
                    var not = (failure as ValidationFailureCustom).Notificacao;
                    AdicionarNotificacao(not);
                    continue;
                }
                /*obter enum pelo name*/
                AdicionarNotificacao(new Erro(CriticidadeEnum.Media, null, failure.ErrorMessage, null, null));
            }
            return Result.IsValid && !_Notificacoes.TemErros;
        }

        protected CompositeSpecification()
        {
            _Notificacoes = new ListNotificacoes<Notificacao>();
            _Res = new List<Resources.Models.Resources<string>>();
            var res = ObterRes();
            if (res != null)
            {
                var rang = ObterResArquivo(res);
                if (rang != null)
                    _Res.AddRange(rang);
            }
        }

        public abstract IEnumerable<Resources.Models.Resources<string>> ObterRes();

        public virtual void AdicionarNotificacao(Notificacao erro)
        {
            if (!_Notificacoes.Any(x => x.TipoNotificacao.HasValue && x.TipoNotificacao.Equals(erro.TipoNotificacao)))
                _Notificacoes.Add(erro);

        }

        /**/
        public ISpecification<T> And(ISpecification<T> specification) =>
            new AndSpecification<T>(this, specification);


        public ISpecification<T> AndOthers<TRigth>(ISpecification<TRigth> specification, TRigth rigth) =>
            new AndOtherSpecification<T, TRigth>(this, specification, rigth);

        public ISpecification<T> AndNotOther<TRigth>(ISpecification<TRigth> specification, TRigth rigth)
        => new AndNotOtherSpecification<T, TRigth>(this, specification, rigth);

        public ISpecification<T> OrNotOther<TRigth>(ISpecification<TRigth> specification, TRigth rigth) =>
            new OrNotOthersSpecification<T, TRigth>(this, specification, rigth);


        public ISpecification<T> OrOther<TRigth>(ISpecification<TRigth> specification, TRigth rigth)
            => new OrOtherSpecification<T, TRigth>(this, specification, rigth);

        public ISpecification<T> AndNot(ISpecification<T> specification) =>
           new AndNotSpecification<T>(this, specification);
        public ISpecification<T> Or(ISpecification<T> specification) =>
            new OrSpecification<T>(this, specification);
        public ISpecification<T> OrNot(ISpecification<T> specification) =>
            new OrNotSpecification<T>(this, specification);
        public ISpecification<T> Not(ISpecification<T> specification) =>
            new NotSpecification<T>(specification);

        public virtual void AdicionarNotificacoes(IReadOnlyList<Notificacao> notificacaos)
        {
            foreach (var item in notificacaos)
            {
                AdicionarNotificacao(item);
            }
        }

        public virtual IEnumerable<Resources<string>> ObterResArquivo(IEnumerable<Resources<string>> resources)
        {
            if (resources != null && resources.Count() > 0)
                return Resources.ResourcesManage.ObterResources(null, resources);

            return null;
        }

        public virtual void ObterNotificacoes(ISpecification<T> specification)
        {
            foreach (var item in specification.Notificacoes)
            {
                AdicionarNotificacao(item);
            }
        }
    }
}
