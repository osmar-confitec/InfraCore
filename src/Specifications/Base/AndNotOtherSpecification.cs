using Resources.Models;
using Specifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Specifications.Base
{
   
      public class AndNotOtherSpecification<T,TRigth> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> _leftSpecification;
        private readonly ISpecification<TRigth> _rightSpecification;
        private readonly TRigth _right;

        public AndNotOtherSpecification(ISpecification<T> left,
            ISpecification<TRigth> rightSpecification, TRigth right)
        {
            _leftSpecification = left;
            _rightSpecification = rightSpecification;
            _right = right;
        }

        public override async Task<bool> IsSatisfiedBy(T candidate) =>
            (await _leftSpecification.IsSatisfiedBy(candidate) &&
             await _rightSpecification.IsSatisfiedBy(_right)) != true;

        public override IEnumerable<Resources<string>> ObterRes() => null;
    }
     
   

}
