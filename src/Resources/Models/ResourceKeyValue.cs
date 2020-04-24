using Resources.Enuns;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.Models
{
   public class ResourceKeyValue
    {

        public ModulosEnum? Modulos { get; set; }
        public ResourceValueEnum ResourceValue { get; set; }

            public override bool Equals(object obj)
            {
                var compareTo = obj as ResourceKeyValue;

                if (ReferenceEquals(this, compareTo)) return true;
                if (ReferenceEquals(null, compareTo)) return false;

                return (Modulos == compareTo.Modulos && ResourceValue == compareTo.ResourceValue);
            }


            public static bool operator !=(ResourceKeyValue a, ResourceKeyValue b)
            {
                return !(a.Modulos == b.Modulos && a.ResourceValue == b.ResourceValue);
            }

            public static bool operator ==(ResourceKeyValue a, ResourceKeyValue b)
            {
                if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                    return true;

                if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                    return false;

                return a.Equals(b);
            }

            public override int GetHashCode()
            {
                return (GetType().GetHashCode() * 907) + Guid.NewGuid().GetHashCode();
            }

    }
}
