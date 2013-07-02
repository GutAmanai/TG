using System;
using System.Collections.Generic;

namespace br.dominio.tg.Entidades
{
    public class Promocao : EntidadeBase
    {
        public virtual string Nome { get;  protected set; }
        public virtual DateTime DataEntrada { get; protected set; }
        public virtual string Descricao { get; protected set; }
        public virtual string ImagemUrl { get; protected set; }
        public virtual double Latitude { get; protected set; }
        public virtual double Longitude { get; protected set; }

        private ICollection<ClientePromocao> _clientePromocao;
        public virtual IEnumerable<ClientePromocao> ClientePromocao
        {
            get { return _clientePromocao; }
        }

        protected Promocao()
        {
            
        }
    }
}
