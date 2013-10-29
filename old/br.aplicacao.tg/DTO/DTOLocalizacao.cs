﻿namespace br.aplicacao.tg.DTO
{
    public class DTOLocalizacao
    {
        public int IdLocalizacao { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public DTOLocalizacao()
        {
        }

        public DTOLocalizacao(int id, double latitude, double longitude) : this()
        {
            this.IdLocalizacao = id;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

    }
}
