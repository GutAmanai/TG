using System;

namespace br.dominio.tg.Entidades
{
    public class QualificacaoPromocao : EntidadeBase
    {
        public virtual Consumidor Consumidor { get; set; }
        public virtual ClientePromocao ClientePromocao { get; set; }
        public virtual bool Like { get; set; }
        public virtual DateTime DataEntrada { get; set; }

        protected QualificacaoPromocao()
        {
            
        }
    }
}
