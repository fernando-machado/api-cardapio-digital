using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Aplicacao.Servicos;

namespace CardapioDigital.Api.Controllers
{
    /// <summary>
    /// Contas Controller
    /// </summary>
    [RoutePrefix("api/v1/contas")]
    public class ContasController : ApiController
    {
        private readonly GerenciamentoConta _gerenciamentoConta;

        /// <summary>
        /// Initialize instance of <see cref="ContasController"/>
        /// </summary>
        /// <param name="gerenciamentoConta">Repositório de gerenciamento de contas</param>
        public ContasController(GerenciamentoConta gerenciamentoConta)
        {
            _gerenciamentoConta = gerenciamentoConta;
        }

        /// <summary>
        /// Obter todas as contas
        /// </summary>
        /// <remarks>
        /// Obtém todas as contas cadastradas
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("")]
        [ResponseType(typeof(IEnumerable<ContaDto>))]
        public IHttpActionResult ObterTodasContas()
        {
            var contas = _gerenciamentoConta.ObterTodasAsContas();

            return Ok(contas);
        }

        /// <summary>
        /// Obter conta por id
        /// </summary>
        /// <remarks>
        /// Obtém as informações da conta solicitada
        /// </remarks>
        /// <param name="idConta">id da conta</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("{idConta:int}", Name = "ObterContaPorId")]
        [ResponseType(typeof(ContaDto))]
        public IHttpActionResult ObterContaPorId(int idConta)
        {
            var conta = _gerenciamentoConta.ObterContaPorCodigo(idConta);

            if (conta == null)
                return NotFound();

            return Ok(conta);
        }

        /// <summary>
        /// Cria uma nova conta
        /// </summary>
        /// <param name="mesa">Informações da conta</param>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost, Route("")]
        [ResponseType(typeof(ContaDto))]
        public IHttpActionResult CriarConta([FromBody] MesaDto mesa)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contaCriada = _gerenciamentoConta.CriarConta(mesa.NumeroMesa);

            return CreatedAtRoute("ObterContaPorId", new { idConta = contaCriada.CodigoConta }, contaCriada);
        }

        /// <summary>
        /// Altera uma conta existente
        /// </summary>
        /// <param name="idConta">Id da conta</param>
        /// <param name="contaParaAtualizar">Informações da conta para serem alteradas</param>
        /// <response code="200">Ok</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut, Route("{idConta:int}")]
        [ResponseType(typeof(ContaDto))]
        public IHttpActionResult AlterarConta(int idConta, [FromBody]FechamentoContaDto contaParaAtualizar)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var conta = _gerenciamentoConta.ObterContaPorCodigo(idConta);

            if (conta == null)
                return NotFound();

            _gerenciamentoConta.FecharConta(idConta, contaParaAtualizar);

            return Ok();
        }
    }
}
