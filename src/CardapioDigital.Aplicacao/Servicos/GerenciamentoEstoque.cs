using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Dominio.Core;
using CardapioDigital.Dominio.Core.Idioma;
using CardapioDigital.Dominio.Estoque;
using System.Collections.Generic;
using System.Linq;

namespace CardapioDigital.Aplicacao.Servicos
{
    public class GerenciamentoEstoque
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IRepositorioIdiomas _idiomas;
        private readonly ICategorias _categorias;
        private readonly IProdutos _produtos;

        public GerenciamentoEstoque(
            IUnidadeDeTrabalho unidadeDeTrabalho,
            IRepositorioIdiomas idiomas,
            ICategorias categorias,
            IProdutos produtos)
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _idiomas = idiomas;
            _categorias = categorias;
            _produtos = produtos;
        }

        public ProdutoDto ObterProdutoPorId(int id)
        {
            var produto = _produtos.ObterPorId(id);
            
            return 
                produto == null 
                    ? null 
                    : MapeamentoDtoHelper.MapProdutoCompletoParaDto(produto);
        }

        public IEnumerable<ProdutoDto> ObterTodosProdutos()
        {
            var produtos = _produtos.ObterTodos();

            return produtos.Select(MapeamentoDtoHelper.MapProdutoSimplesParaDto).ToList();
        }

        public IEnumerable<ProdutoDto> ObterProdutos(int idCategoria, int idSubcategoria)
        {
            return idSubcategoria > 0 
                ? ObterProdutosDaSubcategoria(idSubcategoria) 
                : ObterProdutosDaCategoria(idCategoria);
        }

        private IEnumerable<ProdutoDto> ObterProdutosDaCategoria(int codigoCategoria)
        {
            var categoria = _categorias.ObterPorId(codigoCategoria);
            var produtos = categoria.Subcategorias.SelectMany(p => p.Produtos).ToList();

            return produtos.ToList().Select(MapeamentoDtoHelper.MapProdutoCompletoParaDto).ToList();
        }

        private IEnumerable<ProdutoDto> ObterProdutosDaSubcategoria(int codigoSubcategoria)
        {
            var produtos = _produtos.ObterTodosOnde(p => p.Subcategoria.Codigo == codigoSubcategoria).ToList();

            return produtos.ToList().Select(MapeamentoDtoHelper.MapProdutoCompletoParaDto).ToList();
        }

        public void SalvarProduto(ProdutoDto dadosProduto)
        {

        }



        public CategoriaDto ObterCategoriaPorId(int idCategoria)
        {
            var categoria = _categorias.ObterPorId(idCategoria);
            
            return 
                categoria == null 
                    ? null 
                    : MapeamentoDtoHelper.MapCategoriaCompletaParaDto(categoria);
        }

        public IEnumerable<CategoriaDto> ObterTodasCategorias()
        {
            var categorias = _categorias.ObterTodos();

            return categorias.Select(MapeamentoDtoHelper.MapCategoriaSimplesParaDto);
        }


        public IEnumerable<SubcategoriaDto> ObterTodasSubcategorias(int idCategoria)
        {
            var categoria = _categorias.ObterPorId(idCategoria);
            
            return 
                categoria == null 
                    ? null 
                    : categoria.Subcategorias.Select(MapeamentoDtoHelper.MapSubcategoriaCompletaParaDto);
        }

        public SubcategoriaDto ObterSubcategoriaPorId(int idSubcategoria)
        {
            var categoria = _categorias.ObterTodosOnde(c => c.Subcategorias.Any(s => s.Codigo == idSubcategoria)).FirstOrDefault();
            if (categoria == null)
                return null;

            var subcategoria = categoria.Subcategorias.SingleOrDefault(sc => sc.Codigo == idSubcategoria);
            if (subcategoria == null)
                return null;

            return MapeamentoDtoHelper.MapSubcategoriaCompletaParaDto(subcategoria);
        }


        public void PrepararBancoDeDados()
        {
            #region Idiomas
            var portugues = _idiomas.ObterPorSigla("pt-BR");
            if (portugues == null)
            {
                portugues = new Idioma("Português", "pt-BR");
                _idiomas.Salvar(portugues);
            }

            var ingles = _idiomas.ObterPorSigla("en-US");
            if (ingles == null)
            {
                ingles = new Idioma("Inglês", "en-US");
                _idiomas.Salvar(ingles);
            }
            #endregion Idiomas

            #region Categoria01
            var categoria01 = new Categoria("Categoria 01", "Descrição Categoria 01", portugues);

            var subcategoria01 = categoria01.AdicionarSubcategoria(new Subcategoria("Subcategoria 01", "Descrição Subcategoria 01", categoria01, portugues));
            subcategoria01.AdicionarOpcao(new OpcaoSubcategoria("Opção 01 Subcategoria 01", "Descrição Opção 01 Subcategoria 01", portugues));
            subcategoria01.AdicionarOpcao(new OpcaoSubcategoria("Opção 02 Subcategoria 01", "Descrição Opção 02 Subcategoria 01", portugues));

            var produto01 = subcategoria01.AdicionarProduto("Produto 01", "Descrição Produto 01", 11.0m, null, true, portugues);
            produto01.AdicionarOpcao(new OpcaoProduto("Opção 01 Produto 01", "Descrição Opção 01 Produto 01", portugues));
            produto01.AdicionarOpcao(new OpcaoProduto("Opção 02 Produto 01", "Descrição Opção 02 Produto 01", portugues));

            var produto02 = subcategoria01.AdicionarProduto("Produto 02", "Descrição Produto 02", 12.0m, null, false, portugues);
            produto02.AdicionarOpcao(new OpcaoProduto("Opção 01 Produto 02", "Descrição Opção 01 Produto 02", portugues));

            subcategoria01.AdicionarProduto(produto01);
            subcategoria01.AdicionarProduto(produto02);
            subcategoria01.AdicionarProduto(new Produto("Produto 03", "Descrição Produto 03", 13.0m, null, portugues, subcategoria01));
            subcategoria01.AdicionarProduto(new Produto("Produto 04", "Descrição Produto 04", 14.0m, null, true, portugues, subcategoria01));
            subcategoria01.AdicionarProduto(new Produto("Produto 05", "Descrição Produto 05", 15.0m, null, true, portugues, subcategoria01));
            subcategoria01.AdicionarProduto(new Produto("Produto 06", "Descrição Produto 06", 11.0m, null, portugues, subcategoria01));
            subcategoria01.AdicionarProduto(new Produto("Produto 07", "Descrição Produto 07", 12.0m, null, false, portugues, subcategoria01));

            var subcategoria02 = categoria01.AdicionarSubcategoria(new Subcategoria("Subcategoria 02", "Descrição Subcategoria 02", categoria01, portugues));
            subcategoria02.AdicionarProduto(new Produto("Produto 08", "Descrição Produto 08", 13.0m, null, portugues, subcategoria02));
            subcategoria02.AdicionarProduto(new Produto("Produto 09", "Descrição Produto 09", 14.0m, null, portugues, subcategoria02));
            #endregion Categoria01

            #region Categoria02
            var categoria02 = new Categoria("Categoria 02", "Descrição Categoria 02", portugues);

            var subcategoria03 = categoria02.AdicionarSubcategoria(new Subcategoria("Subcategoria 03", "Descrição Subcategoria 03", categoria02, portugues));
            subcategoria03.AdicionarProduto(new Produto("Produto 10", "Descrição Produto 10", 15.0m, null, true, portugues, subcategoria03));
            subcategoria03.AdicionarProduto(new Produto("Produto 11", "Descrição Produto 11", 11.0m, null, portugues, subcategoria03));
            subcategoria03.AdicionarProduto(new Produto("Produto 12", "Descrição Produto 12", 12.0m, null, true, portugues, subcategoria03));
            subcategoria03.AdicionarProduto(new Produto("Produto 13", "Descrição Produto 13", 13.0m, null, false, portugues, subcategoria03));

            var subcategoria04 = categoria02.AdicionarSubcategoria(new Subcategoria("Subcategoria 04", "Descrição Subcategoria 04", categoria02, portugues));
            subcategoria04.AdicionarProduto(new Produto("Produto 14", "Descrição Produto 14", 14.0m, null, false, portugues, subcategoria04));
            subcategoria04.AdicionarProduto(new Produto("Produto 15", "Descrição Produto 15", 15.0m, null, false, portugues, subcategoria04));
            subcategoria04.AdicionarProduto(new Produto("Produto 16", "Descrição Produto 16", 15.0m, null, true, portugues, subcategoria04));
            #endregion Categoria02

            _categorias.Salvar(categoria01);
            _categorias.Salvar(categoria02);

            _unidadeDeTrabalho.Commit();
        }
    }
}
