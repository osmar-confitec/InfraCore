﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SlnNotificacoesApi
{
    public class ConfigNotificacao
    {
        public ConfigNotificacao(Notificacao notificacao)
        {

            Notificacao = notificacao;
        }

        public Notificacao Notificacao { get; private set; }
    }
}
