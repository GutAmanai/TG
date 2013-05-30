using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace br.aplicacao.tg.DTO
{
    public class DTOConsumidor
    {
        public string Nome { get; set; }
        public DateTime DataEntrada { get; set; }
        public string Email { get; set; }
        public string Contato { get; set; }
        public string Senha { get; set; }
    }
}
