using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace br.aplicacao.tg.DTO
{
    public class DTOCliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public DateTime DataEntrada { get; set; }
        public string Cnpj { get; set; }
        public string Responsavel { get; set; }
        public string Email { get; set; }
        public string Contato { get; set; }
        public string FotoUrl { get; set; }
        public string Senha { get; set; }
        public string TempImg { get; set; }
        public string Extension { get; set; }
    }
}
