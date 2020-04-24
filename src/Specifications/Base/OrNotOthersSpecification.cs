using Resources.Models;
using Specifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Specifications.Base
{
    
      public class OrNotOthersSpecification<T,TRight> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> _leftSpecification;
        private readonly ISpecification<TRight> _rightSpecification;
        readonly TRight _right;

        public OrNotOthersSpecification(ISpecification<T> left, ISpecification<TRight> tright, TRight right)
        {
            _leftSpecification = left;
            _rightSpecification = tright;
            _right = right;
        }

        public override async Task<bool> IsSatisfiedBy(T candidate) =>
            ( await _leftSpecification.IsSatisfiedBy(candidate) ||
             await _rightSpecification.IsSatisfiedBy(_right)) != true;

        public override IEnumerable<Resources<string>> ObterRes() => null;
    }
     
    
}
