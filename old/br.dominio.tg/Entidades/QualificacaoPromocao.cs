using System;

namespace br.dominio.tg.Entidades
{
    public class QualificacaoPromocao : EntidadeBase
    {
        public virtual Usuario Usuario { get; set; }
        public virtual ClientePromocao ClientePromocao { get; set; }
        public virtual bool Gostou { get; set; }
        public virtual DateTime DataEntrada { get; set; }

        protected QualificacaoPromocao()
        {
            
        }
    }
}
