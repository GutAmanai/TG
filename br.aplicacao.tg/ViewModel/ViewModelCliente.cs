using System.Collections.Generic;
using System.Web.Script.Serialization;
using br.aplicacao.tg.DTO;

namespace br.aplicacao.tg.ViewModel
{
    public class ViewModelCliente
    {
        private JavaScriptSerializer js = new JavaScriptSerializer();

        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string Responsavel { get; set; }
        public string Email { get; set; }
        public string Contato { get; set; }
        public string FotoUrl { get; set; }
        public string Senha { get; set; }
        public List<DTOLocalizacao> Localizacoes { get; set; }

        public ViewModelCliente()
        {
            this.Localizacoes = new List<DTOLocalizacao>();
        }
        public string LocalizacoesToJson()
        {
            return js.Serialize(Localizacoes);
        }
    }
}
