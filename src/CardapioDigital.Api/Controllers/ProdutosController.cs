using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Aplicacao.Servicos;
using System.Web.Http;
using CardapioDigital.Api.ActionResults;

namespace CardapioDigital.Api.Controllers
{
    public class ProdutosController : ApiController
    {
        private readonly GerenciamentoEstoque _gerenciamentoEstoque;

        public ProdutosController(GerenciamentoEstoque gerenciamentoEstoque)
        {
            _gerenciamentoEstoque = gerenciamentoEstoque;
        }

        // GET api/produtos/{idProduto}
        public IHttpActionResult Get()
        {
            var produtos = _gerenciamentoEstoque.ObterTodosProdutos();
            
            return Ok(produtos);
        }

        // GET api/produtos/{idProduto}
        public IHttpActionResult Get(int idProduto)
        {
            var produtoDto = _gerenciamentoEstoque.ObterProdutoPorId(idProduto);

            if (produtoDto == null)
                return NotFound();

            return Ok(produtoDto);
        }


        public IHttpActionResult GetProdutosCategoria(int idCategoria)
        {
            var produtos = _gerenciamentoEstoque.ObterProdutosDaCategoria(idCategoria);

            return Ok(produtos);
        }

        
        public IHttpActionResult GetProdutosSubcategoria(int idSubcategoria)
        {
            var produtos = _gerenciamentoEstoque.ObterProdutosDaSubcategoria(idSubcategoria);

            return Ok(produtos);
        }


        // POST api/produtos
        public IHttpActionResult Post([FromBody]ProdutoDto novoProduto)
        {
            return new CreatedContentActionResult(Request, Url.Link("Produtos", new { idProduto = 999 }));
        }

        //// PUT api/categorias/{idCategoria}/subcategorias/{idSubcategoria}/produtos/{idProduto}
        //public void Put(int idCategoria, int idSubcategoria, int idProduto, [FromBody]ProdutoDto produto)
        //{
        //}

        //// DELETE api/categorias/{idCategoria}/subcategorias/{idSubcategoria}/produtos/{idProduto}
        //public void Delete(int idCategoria, int idSubcategoria, int idProduto)
        //{
        //}
    }
}