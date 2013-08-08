using br.dominio.tg.Entidades;

namespace br.persistencia.tg.Mapeamentos
{
    public class ClienteLocalizacaoMap : EntidadeBaseMap<ClienteLocalizacao>
    {
        public ClienteLocalizacaoMap()
        {
            Table("ClientePromocao");
            References(x => x.Cliente);
            Map(x => x.DataEntrada);
            Map(x => x.Latitude);
            Map(x => x.Longitude);
        }
    }
}
