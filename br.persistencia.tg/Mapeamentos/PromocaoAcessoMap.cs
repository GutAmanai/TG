using br.dominio.tg.Entidades;

namespace br.persistencia.tg.Mapeamentos
{
    public class PromocaoAcessoMap : EntidadeBaseMap<PromocaoAcesso>
    {
        public PromocaoAcessoMap()
        {
            Table("promocaoacesso");
            Map(x => x.DataEntrada);
            References(x => x.Promocao).Cascade.SaveUpdate();
        }

    }
}
