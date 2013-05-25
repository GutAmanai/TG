using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Map(x => x.ImagemUrl);
            Map(x => x.Latitude);
            Map(x => x.Longitude);

            HasMany(x => x.ClientePromocao);
        }
    }
}
