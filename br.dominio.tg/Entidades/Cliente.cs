using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace br.dominio.tg.Entidades
{
    public class Cliente : EntidadeBase
    {
        public virtual string Nome { get; set; }
        public virtual DateTime DataEntrada { get; set; }
        public virtual string Documento { get; set; }
        public virtual string Responsavel { get; set; }
        public virtual string Email { get; set; }
        public virtual string Contato { get; set; }
        public virtual string FotoUrl { get; set; }

        private ICollection<ClientePromocao> _clientePromocao;
        public IEnumerable<ClientePromocao> ClientePromocao
        {
            get { return _clientePromocao; }
        }

        public void AdicionarClientePromocao(ClientePromocao clientePromocao)
        {
            if(_clientePromocao == null)
                _clientePromocao = new Collection<ClientePromocao>();

            _clientePromocao.Add(clientePromocao);
        }
    }
}
