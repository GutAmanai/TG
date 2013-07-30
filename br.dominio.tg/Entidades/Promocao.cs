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

        public Promocao()
        {
            
        }

        public virtual void AdicionarNome(string nome)
        {
            this.Nome = nome;
        }

        public virtual void AdicionarDataEntrada(DateTime now)
        {
            this.DataEntrada = now;
        }

        public virtual void AdicionarDescricao(string descricao)
        {
            this.Descricao = descricao;
        }

        public virtual void AdicionarLatitude(double latitude)
        {
            this.Latitude = latitude;
        }

        public virtual void AdicionarLongitude(double longitude)
        {
            this.Longitude = longitude;
        }
    }
}
