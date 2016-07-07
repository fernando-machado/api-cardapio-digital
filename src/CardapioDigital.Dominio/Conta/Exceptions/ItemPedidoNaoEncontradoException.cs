using System;

namespace CardapioDigital.Dominio.Conta.Exceptions
{
    public class ItemPedidoNaoEncontradoException : ApplicationException
    {
        public ItemPedidoNaoEncontradoException()
            : base("Não foi possível encontrar o item do pedido solicitado")
        {
        }

        public ItemPedidoNaoEncontradoException(string message)
            : base(message)
        {
        }

        public ItemPedidoNaoEncontradoException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}