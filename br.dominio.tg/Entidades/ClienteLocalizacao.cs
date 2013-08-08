using System;

namespace br.dominio.tg.Entidades
{
    public class ClienteLocalizacao : EntidadeBase
    {
        public virtual Cliente Cliente { get; protected set; }
        public virtual DateTime DataEntrada { get; protected set; }
        public virtual double Latitude { get; protected set; }
        public virtual double Longitude { get; protected set; }

        public ClienteLocalizacao()
        {
            this.DataEntrada = DateTime.Now;
        }

        public ClienteLocalizacao(Cliente cliente, double latitude, double longetude) : this()
        {
            this.Cliente = cliente;
            this.Latitude = latitude;
            this.Longitude = longetude;
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
