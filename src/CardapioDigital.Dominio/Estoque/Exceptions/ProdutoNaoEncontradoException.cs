using System;

namespace CardapioDigital.Dominio.Estoque.Exceptions
{
    public class ProdutoNaoEncontradoException : ApplicationException
    {
        public ProdutoNaoEncontradoException()
            : base("Não foi possível encontrar o produto solicitado")
        {
        }

        public ProdutoNaoEncontradoException(string message)
            :base(message)
        {
        }

        public ProdutoNaoEncontradoException(string format, params object[] args)
            :base(string.Format(format, args))
        {
        }
    }
}