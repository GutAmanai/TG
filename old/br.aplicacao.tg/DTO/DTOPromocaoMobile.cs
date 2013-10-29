using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace br.aplicacao.tg.DTO
{
    public class DTOPromocaoMobile
    {
        public int IdEmpresa { get; set; }
        public int IdPromocao { get; set; }
        public string NomeEmpresa { get; set; }
        public string UrlEmpresa { get; set; }
        public string UrlPromocao { get; set; }
        public string NomePromocao { get; set; }        
        public string DescricaoPromocao  { get; set; }
        public string Validade { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
