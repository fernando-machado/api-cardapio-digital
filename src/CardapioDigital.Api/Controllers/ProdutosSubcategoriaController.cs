using CardapioDigital.Aplicacao.Servicos;
using System.Web.Http;

namespace CardapioDigital.Api.Controllers
{
    public class ProdutosSubcategoriaController : ApiController
    {
        private readonly GerenciamentoEstoque _gerenciamentoEstoque;

        public ProdutosSubcategoriaController(GerenciamentoEstoque gerenciamentoEstoque)
        {
            _gerenciamentoEstoque = gerenciamentoEstoque;
        }

        // GET api/categorias/{idCategoria}/subcategorias/{idSubcategoria}/produtos/{idProduto}
        public IHttpActionResult Get(int idCategoria, int idSubcategoria)
        {
            var produtos = _gerenciamentoEstoque.ObterProdutos(idCategoria: 0, idSubcategoria: idSubcategoria);

            return Ok(produtos);
        }

        // GET api/categorias/{idCategoria}/subcategorias/{idSubcategoria}/produtos/{idProduto}
        public IHttpActionResult Get(int idCategoria, int idSubcategoria, int idProduto)
        {
            var produto = _gerenciamentoEstoque.ObterProdutoPorId(idProduto);
            if (produto == null)
                return NotFound();

            return Ok(produto);
        }
    }
}