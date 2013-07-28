using System;

namespace br.aplicacao.tg.ViewModel
{
    public class ViewModelPromocao
    {
        public int IdPromocao { get; set; }
        public string Nome { get; set; }
        public DateTime DataEntrada { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public int IdCliente { get; set; }
    }
}
