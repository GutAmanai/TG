using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace br.dominio.tg.Entidades
{
    public class Cliente : EntidadeBase
    {
        public virtual string Nome { get;  protected set; }
        public virtual DateTime DataEntrada { get; protected set; }
        public virtual string Documento { get; protected set; }
        public virtual string Responsavel { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual string Contato { get; protected set; }
        public virtual string FotoUrl { get; protected set; }

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
