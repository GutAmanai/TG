using br.dominio.tg.Entidades;

namespace br.persistencia.tg.Mapeamentos
{
    public class ClienteLocalizacaoMap : EntidadeBaseMap<ClienteLocalizacao>
    {
        public ClienteLocalizacaoMap()
        {
            Table("clientelocalizacao");
            Map(x => x.DataEntrada);
            Map(x => x.Latitude);
            Map(x => x.Longitude);
            References(x => x.Cliente);
        }
    }
}
