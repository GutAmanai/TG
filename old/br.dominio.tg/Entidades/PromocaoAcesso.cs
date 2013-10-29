using System;

namespace br.dominio.tg.Entidades
{
    public class PromocaoAcesso : EntidadeBase
    {
        public virtual Promocao Promocao    { get; set; }
        public virtual DateTime DataEntrada { get; set; }

        public PromocaoAcesso()
        {
        }

        public PromocaoAcesso(Promocao promocao) : this()
        {
            this.Promocao = promocao;
            this.DataEntrada = DateTime.Now;
        }
    }
}
