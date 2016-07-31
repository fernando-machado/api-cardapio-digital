using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Aplicacao.Servicos;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace CardapioDigital.Api.Controllers
{
    /// <summary>
    /// Subcategorias Controller
    /// </summary>
    [RoutePrefix("api/v1/categorias/{idCategoria}/subcategorias")]
    public class SubcategoriasController : ApiController
    {
        private readonly GerenciamentoEstoque _gerenciamentoEstoque;

        /// <summary>
        /// Initialize instance of <see cref="SubcategoriasController"/>
        /// </summary>
        /// <param name="gerenciamentoEstoque">Repositório de gerenciamento de estoque</param>
        public SubcategoriasController(GerenciamentoEstoque gerenciamentoEstoque)
        {
            _gerenciamentoEstoque = gerenciamentoEstoque;
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
        [HttpGet, Route("")]
        [ResponseType(typeof(IEnumerable<SubcategoriaDto>))]
        public IHttpActionResult ObterSubcategoriasPorCategoria(int idCategoria)
        {
            var subcategorias = _gerenciamentoEstoque.ObterTodasSubcategorias(idCategoria);

            return Ok(subcategorias);
        }

        /// <summary>
        /// Obter subcategoria por id
        /// </summary>
        /// <remarks>
        /// Obtém as informações da subcategoria solicitada
        /// </remarks>
        /// <param name="idCategoria">id da categoria</param>
        /// <param name="idSubcategoria">id da subcategoria</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("{idSubcategoria:int}", Name = "ObterSubcategoriaPorId")]
        [ResponseType(typeof(SubcategoriaDto))]
        public IHttpActionResult ObterSubcategoriaPorId(int idCategoria, int idSubcategoria)
        {
            var subcategoria = _gerenciamentoEstoque.ObterSubcategoriaPorId(idCategoria, idSubcategoria);

            if (subcategoria == null)
                return NotFound();

            return Ok(subcategoria);
        }

        /// <summary>
        /// Obter produtos por subcategoria
        /// </summary>
        /// <remarks>
        /// Obtém os produtos da subcategoria solicitada
        /// </remarks>
        /// <param name="idCategoria">id da categoria</param>
        /// <param name="idSubcategoria">id da subcategoria</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("{idSubcategoria:int}/produtos")]
        [ResponseType(typeof(IEnumerable<ProdutoDto>))]
        public IHttpActionResult ObterProdutosPorSubcategoria(int idCategoria, int idSubcategoria)
        {
            //TODO: Refatorar ObterProdutos
            var produtos = _gerenciamentoEstoque.ObterProdutos(idCategoria, idSubcategoria);

            return Ok(produtos);
        }

        /// <summary>
        /// Cria uma nova subcategoria
        /// </summary>
        /// <param name="idCategoria">id da categoria</param>
        /// <param name="novaSubcategoria">Informações da subcategoria</param>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost, Route("")]
        [ResponseType(typeof(SubcategoriaDto))]
        public IHttpActionResult CriarSubcategoria(int idCategoria, [FromBody]SubcategoriaDto novaSubcategoria)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
            
            //TODO: Salvar informações no DB
            object subcategoriaCriada = null;

            return CreatedAtRoute("ObterSubcategoriaPorId", new { idSubcategoria = 999 }, subcategoriaCriada);
        }

        /// <summary>
        /// Altera uma subcategoria existente
        /// </summary>
        /// <param name="idCategoria">id da categoria</param>
        /// <param name="idSubcategoria">id da subcategoria</param>
        /// <param name="subcategoriaParaAlterar">Informações da subcategoria para serem alteradas</param>
        /// <response code="200">Ok</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut, Route("{idSubcategoria:int}")]
        [ResponseType(typeof(SubcategoriaDto))]
        public IHttpActionResult AlterarSubcategoria(int idCategoria, int idSubcategoria, [FromBody]SubcategoriaDto subcategoriaParaAlterar)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var subcategoriaExistente = _gerenciamentoEstoque.ObterSubcategoriaPorId(idCategoria, idSubcategoria);

            if (subcategoriaExistente == null)
                return NotFound();

            //TODO: Atualizar informações no DB

            return Ok(subcategoriaExistente);
        }

        /// <summary>
        /// Exclui uma subcategoria
        /// </summary>
        /// <param name="idCategoria">id da categoria</param>
        /// <param name="idSubcategoria">id da subcategoria</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpDelete, Route("{idSubcategoria:int}")]
        public IHttpActionResult ExcluirSubcategoria(int idCategoria, int idSubcategoria)
        {
            //TODO: Excluir informações no DB
            
            return Ok();
        }
    }
}
