using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using br.dominio.tg.Entidades;

namespace br.persistencia.tg.Mapeamentos
{
    public class QualificacaoPromocaoMap : EntidadeBaseMap<QualificacaoPromocao>
    {
        public QualificacaoPromocaoMap()
        {
            Table("QualificacaoPromocao");
            References(x => x.ClientePromocao);
            References(x => x.Usuario);
            Map(x => x.Gostou);
            Map(x => x.DataEntrada);
        }
    }
}
