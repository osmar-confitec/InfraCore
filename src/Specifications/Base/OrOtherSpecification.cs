using Resources.Models;
using Specifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Specifications.Base
{

     
        public class OrOtherSpecification<T,TRight> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> _leftSpecification;
        private readonly ISpecification<TRight> _rightSpecification;
        readonly TRight _right;

        public OrOtherSpecification(ISpecification<T> left, ISpecification<TRight> rightes, TRight right)
        {
            _leftSpecification = left;
            _rightSpecification = rightes;
            _right = right;
        }

        public override IEnumerable<Resources<string>> ObterRes() => null;

        public override async Task<bool> IsSatisfiedBy(T candidate)
        {
            return await _leftSpecification.IsSatisfiedBy(candidate) ||
                   await _rightSpecification.IsSatisfiedBy(_right);
        }
    }
        
}
