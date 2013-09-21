using br.dominio.tg.Entidades;

namespace br.persistencia.tg.Mapeamentos
{
    public class PromocaoMap : EntidadeBaseMap<Promocao>
    {
        public PromocaoMap()
        {
            Table("Promocao");
            
            Map(x => x.Nome);
            Map(x => x.DataEntrada);
            Map(x => x.Descricao);
            Map(x => x.ImagemUrl).Nullable();

            HasMany(x => x.ClientePromocao).Cascade.AllDeleteOrphan();
            HasMany(x => x.PromocaoAcessos).Cascade.SaveUpdate();
        }
    }
}
