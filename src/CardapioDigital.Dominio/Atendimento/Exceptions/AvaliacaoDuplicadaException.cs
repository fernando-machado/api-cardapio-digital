using System;

namespace CardapioDigital.Dominio.Atendimento.Exceptions
{
    public class AvaliacaoDuplicadaException : ApplicationException
    {
        public AvaliacaoDuplicadaException()
            : base("A avaliação dessa conta já foi recebida!")
        {
        }
    }
}