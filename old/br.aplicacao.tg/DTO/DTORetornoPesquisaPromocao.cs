using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace br.aplicacao.tg.DTO
{
    public class DTORetornoPesquisaPromocao
    {
        public int IdCliente { get; set; }
        public int NPaginas { get; set; }
        public int NLinhas { get; set; }
        
        public List<DTOLocalizacao> Localizacoes { get; set; }
        public List<DTOPromocao> Promocao { get; set; }

        public DTORetornoPesquisaPromocao()
        {
            this.Promocao = new List<DTOPromocao>();
            this.Localizacoes = new List<DTOLocalizacao>();
        }
    }
}
