using System.Collections.Generic;

namespace CardapioDigital.Aplicacao.DTO.Core
{
    public class DadosComPaginacao<T>
    {
        public DadosComPaginacao() { }

        public DadosComPaginacao(Paginacao paginacao, IEnumerable<T> dados)
        {
            this.Paginacao = paginacao;
            this.Dados = dados;
        }

        public Paginacao Paginacao { get; set; }
        public IEnumerable<T> Dados { get; set; }
    }
}
