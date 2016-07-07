using System;

namespace CardapioDigital.Dominio.Core.Idioma
{
    public class IdiomaExistenteException : ApplicationException
    {
        public IdiomaExistenteException()
            : base("Já existe tradução para esse idioma!")
        {
        }

        public IdiomaExistenteException(string mensagem)
            : base(mensagem)
        {

        }
    }
}