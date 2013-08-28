using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace br.aplicacao.tg.DTO
{
    public class DTOClientePromocao
    {
        public int IdCliente { get; set; }
        public List<DTOLocalizacao> Localizacoes { get; set; }
        public List<DTOPromocao> DTOPromocao { get; set; }

        public DTOClientePromocao()
        {
            this.DTOPromocao = new List<DTOPromocao>();
        }
    }
}
