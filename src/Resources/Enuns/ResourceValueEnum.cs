using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.Enuns
{
   public enum ResourceValueEnum
    {
        /*validações genéricas*/
        Sucesso = 1,
        Falha = 2,
        DataNascValida =3,
        EmailExistente = 15,
        Maiorde18 = 20,
        Maiorde12 = 21,
        GuidInvalido = 36,

        /*validações de nome*/
        DescricaoNomeRequerido = 4,
        SobreNomeRequerido = 5,
        TamanhoCampoNome = 6,
        TamanhoCampoSobreNome = 7,
        TamanhoCampoApelido = 8,
        
        /*validacoes do módulo de clientes*/
        ClienteExistente = 22,
        ClienteInexistente = 23,


        CNPJInvalido = 9,
        CPFInvalido = 10, 
        EmailInvalido = 11,
        NomeDocumento = 12,
        SenhaNaoPodeServazia = 13,
       
        /*validacoes telefone*/
        TipoTelefoneInvalido = 14,

        /*validações de usuario*/ 
        UsuarioSenhaInvalida = 26,
        UsuarioForcaSenhaInvalida = 27,
        UsuarioCPFExistente = 28,
        UsuarioEmailExistente = 29,
        UsuarioBloqueadoTentativasInvalidas = 30,
        UsuarioSenhaIncorretos = 31,
        UsuarioCPFIdVazio = 32,
        UsuarioCPFNaoEncontrado = 33,
        UsuarioIdNaoEncontrado = 34,
        UsuarioExistenteCPFEmail = 35,
        UsuarioInativo = 36,
        UsuarioNaoEncontrado = 37,
        UsuarioModuloInvalido = 38,
        UsuarioAcaoInvalido = 39,
        UsuarioModuloExistente = 40,
        UsuarioSenhaConfirmNaoBatem = 41

    }
}
