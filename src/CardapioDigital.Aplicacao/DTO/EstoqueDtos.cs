using System.Collections.Generic;

namespace CardapioDigital.Aplicacao.DTO
{
    public class CategoriaDto
    {
        public CategoriaDto()
        {
            this.Subcategorias = new List<SubcategoriaDto>();
        }

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public IEnumerable<SubcategoriaDto> Subcategorias { get; set; }
    }

    public class SubcategoriaDto
    {
        public SubcategoriaDto()
        {
            this.Produtos = new List<ProdutoDto>();
        }

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public IEnumerable<ProdutoDto> Produtos { get; set; }
    }

    public class ProdutoDto
    {
        public ProdutoDto()
        {
            this.Opcoes = new List<OpcaoDto>();
        }

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public string Imagem { get; set; }
        public bool Destaque { get; set; }
        public IEnumerable<OpcaoDto> Opcoes { get; set; }
    }


    public class OpcaoDto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Selecionada { get; set; }
    }

}
