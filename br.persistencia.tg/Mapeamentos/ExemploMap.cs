using br.dominio.tg.Entidades;

namespace br.persistencia.tg.Mapeamentos
{
    public class ExemploMap : EntidadeBaseMap<Exemplo>
    {
        public ExemploMap()
        {
            Table("Exemplo");
            Map(x => x.Nome).Nullable();
            Map(x => x.Data).Nullable();
        }

    }
}
