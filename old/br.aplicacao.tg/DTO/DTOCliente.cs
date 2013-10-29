using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace br.aplicacao.tg.DTO
{
    public class DTOCliente
    {
        private JavaScriptSerializer js = new JavaScriptSerializer();
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
        public List<DTOLocalizacao> Localizacoes { get; set; }

        public DTOCliente()
        {
            this.Localizacoes = new List<DTOLocalizacao>();
        }
        
        public string LocalizacoesToJson()
        {
            return js.Serialize(Localizacoes);
        }
    }
}
