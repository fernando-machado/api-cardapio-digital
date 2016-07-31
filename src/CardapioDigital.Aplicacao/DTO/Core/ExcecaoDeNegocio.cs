using System;

namespace CardapioDigital.Aplicacao.DTO.Core
{
    public class BusinessException : ApplicationException
    {
        public BusinessException(string message)
            : base(message)
        { }

        public BusinessException(string format, params object[] args)
            : this(string.Format(format, args))
        { }

        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public BusinessException(Exception innerException, string format, params object[] args)
            : this(string.Format(format, args), innerException)
        { }
    }
}
