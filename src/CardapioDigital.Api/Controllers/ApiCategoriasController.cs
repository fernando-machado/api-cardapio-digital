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
        /// <returns>Lista de <list type="IEnumerable"><see cref="CategoriaDto"/></list></returns>
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
        /// <returns>Retorna uma categoria (<see cref="CategoriaDto"/>)</returns>
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
        /// Cria uma nova categoria
        /// </summary>
        /// <param name="novaCategoria">Informações da categoria</param>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        /// <returns>Categoria criada (<see cref="CategoriaDto"/>)</returns>
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
        /// <returns>Categoria atualizada (<see cref="CategoriaDto"/>)</returns>
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
        /// <returns></returns>
        [HttpDelete, Route("{idCategoria:int}")]
        public IHttpActionResult DeletarCategoria(int idCategoria)
        {
            //_gerenciamentoEstoque.PrepararBancoDeDados();
            return Ok();
        }
    }
}
