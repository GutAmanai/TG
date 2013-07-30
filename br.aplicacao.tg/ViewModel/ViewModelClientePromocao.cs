using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace br.aplicacao.tg.ViewModel
{
    public class ViewModelClientePromocao
    {
        public int IdCliente { get; set; }
        public List<ViewModelPromocao> ViewModelPromocao { get; set; }
    }
}
