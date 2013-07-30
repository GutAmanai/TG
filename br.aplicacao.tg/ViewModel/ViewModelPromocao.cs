using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace br.aplicacao.tg.ViewModel
{
    public class ViewModelPromocao
    {
        public int IdPromocao               { get; set; }
        public bool Ativo                   { get; set; }
        public DateTime DataExpiracao       { get; set; }
        public string Nome                  { get; set; }
        public DateTime DataEntrada         { get; set; }
        public string Descricao             { get; set; }
        public string ImagemUrl             { get; set; }
        public double Latitude              { get; set; }
        public double Longitude             { get; set; }
    }
}
