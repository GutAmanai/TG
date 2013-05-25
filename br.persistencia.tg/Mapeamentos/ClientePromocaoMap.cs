using br.dominio.tg.Entidades;

namespace br.persistencia.tg.Mapeamentos
{
    public class ClientePromocaoMap : EntidadeBaseMap<ClientePromocao>
    {
        public ClientePromocaoMap()
        {
            Table("ClientePromocao");
            References(x => x.Cliente);
            References(x => x.Promocao);

            Map(x => x.DataExpiracao);
            Map(x => x.DataExpiracao);
        }

    }
}
