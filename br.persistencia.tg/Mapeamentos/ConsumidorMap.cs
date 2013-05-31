using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using br.dominio.tg.Entidades;

namespace br.persistencia.tg.Mapeamentos
{
    public class ConsumidorMap: EntidadeBaseMap<Consumidor>
    {
        public ConsumidorMap()
        {
            Table("Consumidor");

            Map(x => x.Nome);
            Map(x => x.DataEntrada);
            Map(x => x.Email).Unique();
            Map(x => x.Contato);
            Map(x => x.Senha);

            HasMany(x => x.QualificacaoPromocao);
        }
    }
}
