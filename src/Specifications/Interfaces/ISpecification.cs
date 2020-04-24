using SlnNotificacoesApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Specifications.Interfaces
{
    public interface ISpecification<T>
    {
        Task<bool> IsSatisfiedBy(T candidate);

        Task<bool> Validar(T ObjValidar);

        IReadOnlyList<Notificacao> Notificacoes { get; }

        void AdicionarNotificacao(Notificacao erro);

        void ObterNotificacoes(ISpecification<T> specification);

        void AdicionarNotificacoes(IReadOnlyList<Notificacao> notificacaos);

        IEnumerable<Resources.Models.Resources<string>> ObterResArquivo(IEnumerable<Resources.Models.Resources<string>> resources);

        IReadOnlyList<Resources.Models.Resources<string>> Res { get; }

        ISpecification<T> And(ISpecification<T> specification);
        
        ISpecification<T> AndNot(ISpecification<T> specification);
        ISpecification<T> Or(ISpecification<T> specification);
        ISpecification<T> OrNot(ISpecification<T> specification);
        ISpecification<T> Not(ISpecification<T> specification);


        /*cunstomizados*/
        ISpecification<T> AndOthers<TRigth>(ISpecification<TRigth> specification, TRigth rigth);

        ISpecification<T> AndNotOther<TRigth>(ISpecification<TRigth> specification, TRigth rigth);
        //OrOtherSpecification
        ISpecification<T> OrNotOther<TRigth>(ISpecification<TRigth> specification, TRigth rigth);

        ISpecification<T> OrOther<TRigth>(ISpecification<TRigth> specification, TRigth rigth);
    }
}
