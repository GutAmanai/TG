using br.dominio.tg.Entidades;

namespace br.persistencia.tg.Mapeamentos
{
    public class ClientePromocaoMap : EntidadeBaseMap<ClientePromocao>
    {
        public ClientePromocaoMap()
        {
            Table("clientepromocao");
            
            Map(x => x.DataLiberacao);
            Map(x => x.DataExpiracao);
            Map(x => x.DataEntrada);
            Map(x => x.Ativo);

            References(x => x.Cliente);
            References(x => x.Promocao).Cascade.SaveUpdate();
        }

    }
}
