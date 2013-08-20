using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace br.aplicacao.tg.DTO
{
    public class DTOPesquisaPromocao
    {
        public int IdCliente        { get; set; }
        public int NPagina          { get; set; }
        public int QtdPagina        { get; set; }
        public string Nome          { get; set; }
        public string Descricao     { get; set; }
    }
}
