using SlnNotificacoesApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Specifications.Models
{
    public class ValidationFailureCustom : FluentValidation.Results.ValidationFailure
    {

        public Notificacao Notificacao { get; set; }

        public ValidationFailureCustom(string propertyName, string errorMessage) : base(propertyName, errorMessage)
        {

        }

        public ValidationFailureCustom(string propertyName, string errorMessage, Notificacao _Notificacao) : this(propertyName, errorMessage)
        {
            Notificacao = _Notificacao;
        }

    }
}
