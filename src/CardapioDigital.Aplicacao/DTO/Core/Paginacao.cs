namespace CardapioDigital.Aplicacao.DTO.Core
{
    public class Paginacao
    {
        public Paginacao()
        {
            this.ProximaPagina = 1;
            this.QuantidadeRegistrosDesejada = 10;
        }

        public int ProximaPagina { get; set; }
        public int QuantidadeRegistrosDesejada { get; set; }
        public int QuantidadeTotalRegistros { get; set; }
        internal int RegistrosParaSaltar
        {
            get { return (ProximaPagina * QuantidadeRegistrosDesejada) - QuantidadeRegistrosDesejada; }
        }

        private int _quantidadeTotalPaginas;
        public int QuantidadeTotalPaginas
        {
            set { _quantidadeTotalPaginas = value; }
            get
            {
                if (QuantidadeTotalRegistros == 0)
                    _quantidadeTotalPaginas = 0;

                if (QuantidadeTotalRegistros % QuantidadeRegistrosDesejada == 0)
                    _quantidadeTotalPaginas = QuantidadeTotalRegistros / QuantidadeRegistrosDesejada;
                else
                    _quantidadeTotalPaginas = (QuantidadeTotalRegistros / QuantidadeRegistrosDesejada) + 1;

                return _quantidadeTotalPaginas;
            }
        }
    }
}
