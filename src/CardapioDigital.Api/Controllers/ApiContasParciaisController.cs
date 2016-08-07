using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Aplicacao.Servicos;

namespace CardapioDigital.Api.Controllers
{
    /// <summary>
    /// Parciais Controller
    /// </summary>
    [RoutePrefix("api/v1/parciais")]
    public class ContasParciaisController : ApiController
    {
        private readonly GerenciamentoConta _gerenciamentoConta;

        /// <summary>
        /// Initialize instance of <see cref="ContasParciaisController"/>
        /// </summary>
        /// <param name="gerenciamentoConta">Repositório de gerenciamento de contas</param>
        public ContasParciaisController(GerenciamentoConta gerenciamentoConta)
        {
            _gerenciamentoConta = gerenciamentoConta;
        }

        /// <summary>
        /// Obter todas as contas parciais da conta
        /// </summary>
        /// <remarks>
        /// Obtém todas as contas parciais da conta informada
        /// </remarks>
        /// <param name="idConta">Id da conta</param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("~/api/v1/contas/{idConta:int}/parciais")]
        [ResponseType(typeof(IEnumerable<ContaParcialDto>))]
        public IHttpActionResult ObterContasParciasDaConta(int idConta)
        {
            var contasParciais = _gerenciamentoConta.ObterParciaisDaConta(idConta);

            return Ok(contasParciais);
        }

        /// <summary>
        /// Cria uma nova conta parcial
        /// </summary>
        /// <param name="idConta">Id da conta</param>
        /// <param name="novaContaParcial">Informações da conta parcial</param>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost, Route("~/api/v1/contas/{idConta:int}/parciais")]
        [ResponseType(typeof(ContaParcialDto))]
        public IHttpActionResult CriarContaParcial(int idConta, [FromBody]NovaContaParcial novaContaParcial)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contaParcialCriada = _gerenciamentoConta.CriarContaParcial(idConta, novaContaParcial);

            return CreatedAtRoute("ObterContaParcialPorId", new { idConta, idParcial = contaParcialCriada.CodigoContaParcial }, contaParcialCriada);
        }

        /// <summary>
        /// Obter conta parcial por id
        /// </summary>
        /// <remarks>
        /// Obtém as informações da conta parcial solicitada
        /// </remarks>
        /// <param name="idParcial">Id da conta parcial</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("{idParcial:int}", Name = "ObterContaParcialPorId")]
        [ResponseType(typeof(ContaParcialDto))]
        public IHttpActionResult ObterContaParcialPorId(int idParcial)
        {
            var contaParcial = _gerenciamentoConta.ObterContaParcial(idParcial);

            if (contaParcial == null)
                return NotFound();

            return Ok(contaParcial);
        }

        /// <summary>
        /// Altera uma conta parcial existente
        /// </summary>
        /// <param name="idParcial">Id da conta parcial</param>
        /// <param name="contaParcialParaAtualizar">Informações da conta parcial para serem alteradas</param>
        /// <response code="200">Ok</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut, Route("{idParcial:int}")]
        [ResponseType(typeof(ContaParcialDto))]
        public IHttpActionResult AlterarContaParcial(int idParcial, [FromBody]ContaParcialDto contaParcialParaAtualizar)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Exclui uma conta parcial
        /// </summary>
        /// <param name="idParcial">Id da conta parcial</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpDelete, Route("{idParcial:int}")]
        public IHttpActionResult ExcluirContaParcial(int idParcial)
        {
            _gerenciamentoConta.RemoverContaParcial(idParcial);

            return Ok();
        }
    }
}
