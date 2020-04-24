using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WebServices.En
{
    public enum EMediaType
    {
        [Description("application/json")]
        Requestjson = 1,
        [Description("application/json")]
        Responsejson = 2
    }
}
