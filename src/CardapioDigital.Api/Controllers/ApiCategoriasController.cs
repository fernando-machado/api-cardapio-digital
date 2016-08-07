using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Aplicacao.Servicos;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace CardapioDigital.Api.Controllers
{
    /// <summary>
    /// Categorias Controller
    /// </summary>
    [RoutePrefix("api/v1/categorias")]
    public class CategoriasController : ApiController
    {
        private readonly GerenciamentoEstoque _gerenciamentoEstoque;

        /// <summary>
        /// Initialize instance of <see cref="CategoriasController"/>
        /// </summary>
        /// <param name="gerenciamentoEstoque">Repositório de gerenciamento de estoque</param>
        public CategoriasController(GerenciamentoEstoque gerenciamentoEstoque)
        {
            _gerenciamentoEstoque = gerenciamentoEstoque;
        }

        /// <summary>
        /// Obter todas as categorias
        /// </summary>
        /// <remarks>
        /// Obtém todos as categorias cadastrados
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("")]
        [ResponseType(typeof(IEnumerable<CategoriaDto>))]
        public IHttpActionResult ObterTodasCategorias()
        {
            return Ok(_gerenciamentoEstoque.ObterTodasCategorias());
        }

        /// <summary>
        /// Obter categoria por id
        /// </summary>
        /// <remarks>
        /// Obtém as informações da categoria solicitada
        /// </remarks>
        /// <param name="idCategoria">id da categoria</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("{idCategoria:int}", Name = "ObterCategoriaPorId")]
        [ResponseType(typeof(CategoriaDto))]
        public IHttpActionResult ObterCategoriaPorId(int idCategoria)
        {
            var categoria = _gerenciamentoEstoque.ObterCategoriaPorId(idCategoria);
            
            if (categoria == null)
                return NotFound();
            
            return Ok(categoria);
        }

        /// <summary>
        /// Obter subcategorias por categoria
        /// </summary>
        /// <remarks>
        /// Obtém as subcategorias da categoria solicitada
        /// </remarks>
        /// <param name="idCategoria">id da categoria</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("{idCategoria}/subcategorias")]
        [ResponseType(typeof(IEnumerable<SubcategoriaDto>))]
        public IHttpActionResult ObterSubcategoriasPorCategoria(int idCategoria)
        {
            var subcategorias = _gerenciamentoEstoque.ObterTodasSubcategorias(idCategoria);

            return Ok(subcategorias);
        }

        /// <summary>
        /// Obter produtos por categoria
        /// </summary>
        /// <remarks>
        /// Obtém os produtos da categoria solicitada
        /// </remarks>
        /// <param name="idCategoria">id da categoria</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("{idCategoria:int}/produtos")]
        [ResponseType(typeof(IEnumerable<ProdutoDto>))]
        public IHttpActionResult ObterProdutosPorCategoria(int idCategoria)
        {
            //TODO: Refatorar ObterProdutos
            var produtos = _gerenciamentoEstoque.ObterProdutos(idCategoria: idCategoria, idSubcategoria: 0);

            return Ok(produtos);
        }

        /// <summary>
        /// Cria uma nova categoria
        /// </summary>
        /// <param name="novaCategoria">Informações da categoria</param>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost, Route("")]
        [ResponseType(typeof(CategoriaDto))]
        public IHttpActionResult CriarCategoria([FromBody]CategoriaDto novaCategoria)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var content = (object)null;

            return CreatedAtRoute("ObterCategoriaPorId", new { idCategoria = 999 }, content);
        }

        /// <summary>
        /// Altera uma categoria existente
        /// </summary>
        /// <param name="idCategoria">Id da categoria</param>
        /// <param name="categoriaParaAtualizar">Informações da categoria para serem alteradas</param>
        /// <response code="200">Ok</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut, Route("{idCategoria:int}")]
        [ResponseType(typeof(CategoriaDto))]
        public IHttpActionResult AlterarCategoria(int idCategoria, [FromBody]CategoriaDto categoriaParaAtualizar)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoria = _gerenciamentoEstoque.ObterProdutoPorId(idCategoria);

            if (categoria == null)
                return NotFound();

            //Atualizar

            return Ok(categoria);
        }

        /// <summary>
        /// Exclui uma categoria
        /// </summary>
        /// <param name="idCategoria">Id da categoria</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpDelete, Route("{idCategoria:int}")]
        public IHttpActionResult ExcluirCategoria(int idCategoria)
        {
            //_gerenciamentoEstoque.PrepararBancoDeDados();
            return Ok();
        }
    }
}
