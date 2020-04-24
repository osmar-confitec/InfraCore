using Resources.Models;
using Specifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Specifications.Base
{
    public class NotSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> _notSpecification;

        public NotSpecification(ISpecification<T> not)
        {
            _notSpecification = not;
        }

        public override async Task<bool> IsSatisfiedBy(T candidate) =>
            ! await _notSpecification.IsSatisfiedBy(candidate);

        public override IEnumerable<Resources<string>> ObterRes() => null;
    }
}
