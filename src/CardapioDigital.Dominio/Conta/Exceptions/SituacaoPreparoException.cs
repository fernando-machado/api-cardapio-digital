using System;

namespace CardapioDigital.Dominio.Conta.Exceptions
{
    public class SituacaoPreparoException : ApplicationException
    {
        public SituacaoPreparoException()
            : base("Situação preparo inválida")
        {
        }
        public SituacaoPreparoException(string message)
            : base(message)
        {
        }
        public SituacaoPreparoException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}