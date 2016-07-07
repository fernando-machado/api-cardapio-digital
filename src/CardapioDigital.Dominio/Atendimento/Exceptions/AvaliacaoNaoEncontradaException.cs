using System;

namespace CardapioDigital.Dominio.Atendimento.Exceptions
{
    public class AvaliacaoNaoEncontradaException : ApplicationException
    {
        public AvaliacaoNaoEncontradaException()
            : base("Não foi possível encontrar a avaliação solicitada")
        {
        }

        public AvaliacaoNaoEncontradaException(string message)
            : base(message)
        {
        }

        public AvaliacaoNaoEncontradaException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}