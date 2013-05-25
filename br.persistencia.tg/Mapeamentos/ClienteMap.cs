using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using br.dominio.tg.Entidades;

namespace br.persistencia.tg.Mapeamentos
{
    public class ClienteMap: EntidadeBaseMap<Cliente>
    {
        public ClienteMap()
        {
            Table("Cliente");
            Map(x => x.Nome);
            Map(x => x.DataEntrada);
            Map(x => x.Documento);
            Map(x => x.Responsavel);
            Map(x => x.Email);
            Map(x => x.Contato);
            Map(x => x.FotoUrl);

            HasMany(x => x.ClientePromocao);
        }
    }
}
