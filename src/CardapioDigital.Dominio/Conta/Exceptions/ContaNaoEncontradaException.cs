using System;

namespace CardapioDigital.Dominio.Conta.Exceptions
{
    public class ContaNaoEncontradaException : ApplicationException
    {
        public ContaNaoEncontradaException()
            : base("Não foi possível encontrar a conta solicitada")
        {
        }

        public ContaNaoEncontradaException(string message)
            : base(message)
        {
        }

        public ContaNaoEncontradaException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}