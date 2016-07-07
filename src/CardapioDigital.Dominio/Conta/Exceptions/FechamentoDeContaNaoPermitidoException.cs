using System;

namespace CardapioDigital.Dominio.Conta.Exceptions
{
    public class FechamentoDeContaNaoPermitidoException : ApplicationException
    {
        public FechamentoDeContaNaoPermitidoException()
            : base("A conta não pode ser fechada!")
        {
        }

        public FechamentoDeContaNaoPermitidoException(string message)
            : base (message)
        {
        }

        public FechamentoDeContaNaoPermitidoException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}