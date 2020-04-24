using System;
using System.Collections.Generic;
using System.Text;

namespace SlnNotificacoesApi
{
    public abstract class Entity<T> : ValidadorBase<T> where T : Entity<T>
    {
        public Guid Id { get; protected set; }


        //Operadores utilizados para comparação de igualdade e diferença utilizando o equals comparando com id
        public static bool operator ==(Entity<T> a, Entity<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<T> a, Entity<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + "[Id = " + Id + "]";
        }


        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity<T>;

            //verificando instancias
            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(this, null)) return false;

            //Comparando entidades
            return Id.Equals(compareTo.Id);

        }

        protected Entity()
        {
            Id = Guid.NewGuid();
            ObterRes();
        }
    }
}
