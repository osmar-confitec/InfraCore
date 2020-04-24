using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SlnNotificacoesApi.Enum
{
    public enum TipoNotificacaoEnum
    {

        [Description("CPF já existe")]
        CPFEncontrado = 1,
        [Description("Nome Cliente Não Informado")]
        NomeClienteNaoInformado = 2,
        CNPJInvalido = 3,
        CPFInvalido = 4,
        EmailExistente = 5
        


    }
}
