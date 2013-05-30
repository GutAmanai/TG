using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace br.aplicacao.tg.DTO
{
    public class DTOPromocao
    {
        public string Nome { get; set; }
        public DateTime DataEntrada { get; set; }
        public string Descricao { get; set; }
        public string ImagemUrl { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Like { get; set; }
        public DateTime Expira { get; set; }
    }
}
