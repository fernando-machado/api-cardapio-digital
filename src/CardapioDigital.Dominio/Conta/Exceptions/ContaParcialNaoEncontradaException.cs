using System;

namespace CardapioDigital.Dominio.Conta.Exceptions
{
    public class ContaParcialNaoEncontradaException : ApplicationException
    {
        public ContaParcialNaoEncontradaException()
            : base("Não foi possível encontrar a conta parcial solicitada")
        {
        }

        public ContaParcialNaoEncontradaException(string message)
            : base(message)
        {
        }

        public ContaParcialNaoEncontradaException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}