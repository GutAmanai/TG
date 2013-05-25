using System;
using System.Collections.Generic;

namespace br.dominio.tg.Entidades
{
    public class Promocao
    {
        public virtual string Nome { get; set; }
        public virtual DateTime DataEntrada { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string ImagemUrl { get; set; }
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }

        private ICollection<ClientePromocao> _clientePromocao;
        public IEnumerable<ClientePromocao> ClientePromocao
        {
            get { return _clientePromocao; }
        }
    }
}
