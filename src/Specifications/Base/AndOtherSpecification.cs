using Resources.Models;
using Specifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Specifications.Base
{
   public class AndOtherSpecification<T,TRigth> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> _leftSpecification;
        private readonly ISpecification<TRigth> _rightSpecification;
        private readonly TRigth _rigth;


        public AndOtherSpecification(ISpecification<T> left, ISpecification<TRigth> right, TRigth rigth)
        {
            _leftSpecification = left;
            _rightSpecification = right;
            _rigth = rigth;
        }

        public override  async Task<bool> IsSatisfiedBy(T candidate) =>
            await _leftSpecification.IsSatisfiedBy(candidate) &&
            await _rightSpecification.IsSatisfiedBy(_rigth);

        public override IEnumerable<Resources<string>> ObterRes() => null;
    }
}
