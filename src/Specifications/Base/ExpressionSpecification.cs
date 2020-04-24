using Resources.Models;
using SlnNotificacoesApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Specifications.Base
{
    public class ExpressionSpecification<T> : CompositeSpecification<T>
    {
        private readonly Func<T, bool> _expression;

        public ExpressionSpecification(Func<T, bool> expression)
        {
            if (expression == null)
                throw new ArgumentException();

            _expression = expression;
        }

        public override async Task<bool> IsSatisfiedBy(T candidate) =>
            _expression(candidate);

        public override IEnumerable<Resources<string>> ObterRes() => null;
    }
}
