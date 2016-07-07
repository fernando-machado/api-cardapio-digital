using System;

namespace CardapioDigital.Dominio.Atendimento.Exceptions
{
    public class SolicitacaoNaoEncontradaException : ApplicationException
    {
        public SolicitacaoNaoEncontradaException()
            : base("Não foi possível encontrar a solicitação requisitada")
        {
        }

        public SolicitacaoNaoEncontradaException(string message)
            : base(message)
        {
        }

        public SolicitacaoNaoEncontradaException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}