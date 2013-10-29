using br.dominio.tg.Entidades.Interfaces;

namespace br.dominio.tg.Entidades
{
    public abstract class EntidadeBase : IEntidadeBase
    {
        public virtual int Id { get; protected set; }

        public static bool operator ==(EntidadeBase entidade1, EntidadeBase entidade2)
        {
            if (ReferenceEquals(entidade1, entidade2)) return true;
            if (ReferenceEquals(entidade1, null) || ReferenceEquals(entidade2, null)) return false;

            return entidade1.Equals(entidade2);
        }

        public static bool operator !=(EntidadeBase entidade1, EntidadeBase entidade2)
        {
            return !(entidade1 == entidade2);
        }

        public override bool Equals(object entidade)
        {
            var outro = entidade as EntidadeBase;
            return outro != null && outro.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public virtual T CriarCopia<T>() where T : EntidadeBase
        {
            return (T)this.MemberwiseClone();
        }
    }
}
