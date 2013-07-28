using System;

namespace br.aplicacao.tg.DTO
{
    public class DTOPromocao
    {
        public int IdPromocao { get; set; }
        public string Nome { get; set; }
        public DateTime DataEntrada { get; set; }
        public string Descricao { get; set; }
        public string ImagemUrl { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Like { get; set; }
        public DateTime Expira { get; set; }
        public string TempImg { get; set; }
        public string Extension { get; set; }
    }
}
