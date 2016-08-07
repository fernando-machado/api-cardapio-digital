using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Aplicacao.Servicos;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace CardapioDigital.Api.Controllers
{
    /// <summary>
    /// Produtos Controller
    /// </summary>
    [RoutePrefix("api/v1/produtos")]
    public class ProdutosController : ApiController
    {
        private readonly GerenciamentoEstoque _gerenciamentoEstoque;

        /// <summary>
        /// Initialize instance of <see cref="ProdutosController"/>
        /// </summary>
        /// <param name="gerenciamentoEstoque">Repositório de gerenciamento de estoque</param>
        public ProdutosController(GerenciamentoEstoque gerenciamentoEstoque)
        {
            _gerenciamentoEstoque = gerenciamentoEstoque;
        }

        /// <summary>
        /// Obter todos os produtos
        /// </summary>
        /// <remarks>
        /// Obtém todos os produtos cadastrados
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("")]
        [ResponseType(typeof(IEnumerable<ProdutoDto>))]
        public IHttpActionResult ObterTodosProdutos()
        {
            var produtos = _gerenciamentoEstoque.ObterTodosProdutos();

            return Ok(produtos);
        }

        /// <summary>
        /// Obter produto por id
        /// </summary>
        /// <remarks>
        /// Obtém as informações do produto solicitado
        /// </remarks>
        /// <param name="idProduto">id do produto</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("{idProduto:int}", Name = "ObterProdutoPorId")]
        [ResponseType(typeof(ProdutoDto))]
        public IHttpActionResult ObterProdutoPorId(int idProduto)
        {
            var produtoDto = _gerenciamentoEstoque.ObterProdutoPorId(idProduto);

            if (produtoDto == null)
                return NotFound();

            return Ok(produtoDto);
        }

        /// <summary>
        /// Cria um novo produto
        /// </summary>
        /// <param name="novoProduto">Informações do produto</param>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost, Route("")]
        [ResponseType(typeof(ProdutoDto))]
        public IHttpActionResult CriarProduto([FromBody]ProdutoDto novoProduto)
        {
            var produtoCriado = new { Id = 999, Nome = "Implementar" };

            return CreatedAtRoute("ObterProdutoPorId", new { id = 999 }, produtoCriado);
        }

        /// <summary>
        /// Altera um produto existente
        /// </summary>
        /// <param name="idProduto">Id do produto</param>
        /// <param name="produto">Informações do produto para serem alteradas</param>
        /// <response code="200">Ok</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut, Route("{idProduto:int}")]
        [ResponseType(typeof(ProdutoDto))]
        public IHttpActionResult AlterarProduto(int idProduto, [FromBody]ProdutoDto produto)
        {
            var produtoExistente = _gerenciamentoEstoque.ObterProdutoPorId(idProduto);

            if (produtoExistente == null)
                return NotFound();

            produtoExistente.Descricao = produto.Descricao;

            //TODO: Atualiza as informações no BD


            return Ok(produtoExistente);
        }

        /// <summary>
        /// Exclui um produto
        /// </summary>
        /// <param name="idProduto">Id do produto</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpDelete, Route("{idProduto:int}")]
        public IHttpActionResult ExcluirProduto(int idProduto)
        {
            return Ok();
        }
    }
}