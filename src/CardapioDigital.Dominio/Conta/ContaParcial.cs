using System;
using System.Collections.Generic;
using System.Linq;
using CardapioDigital.Dominio.Conta.Exceptions;
using CardapioDigital.Dominio.Core;

namespace CardapioDigital.Dominio.Conta
{
    public class ContaParcial : EntidadeBase
    {
        protected ContaParcial()
        {
            this._itens = new List<ItemPedido>();
        }

        public ContaParcial(string cliente)
            : this()
        {
            this.Cliente = cliente;
        }

        public virtual string Cliente { get; set; }

        private readonly ICollection<ItemPedido> _itens;
        public virtual IEnumerable<ItemPedido> Itens
        {
            get { return _itens; }
        }

        public virtual void AdicionarItemPedido(ItemPedido itemPedido)
        {
            if (itemPedido == null)
                throw new ArgumentNullException("itemPedido");

            _itens.Add(itemPedido);
        }

        public virtual void RemoverItemPedido(int codigoItemPedido)
        {
            var itemPedido = _itens.SingleOrDefault(ip => ip.Codigo == codigoItemPedido);
            if (itemPedido == null)
                throw new ItemPedidoNaoEncontradoException("Não foi possível encontrar o item do pedido solicitado. codigoItemPedido == {0}", codigoItemPedido);

            _itens.Remove(itemPedido);
        }
    }
}
