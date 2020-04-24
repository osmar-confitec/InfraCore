using Resources.Models;
using Specifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Specifications.Base
{
   public class AndSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> _leftSpecification;
        private readonly ISpecification<T> _rightSpecification;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _leftSpecification = left;
            _rightSpecification = right;
        }

        public override async Task<bool> IsSatisfiedBy(T candidate) =>
           await  _leftSpecification.IsSatisfiedBy(candidate) &&
            await _rightSpecification.IsSatisfiedBy(candidate);

        public override IEnumerable<Resources<string>> ObterRes() => null;
    }
}
