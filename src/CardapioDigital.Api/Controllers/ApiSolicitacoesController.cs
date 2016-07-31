using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Aplicacao.Servicos;

namespace CardapioDigital.Api.Controllers
{
    /// <summary>
    /// Solicitacoes Controller
    /// </summary>
    [RoutePrefix("api/v1/solicitacoes")]
    public class SolicitacoesController : ApiController
    {
        private readonly GerenciamentoConta _gerenciamentoConta;
        private readonly GerenciamentoAtendimento _gerenciamentoAtendimento;

        /// <summary>
        /// Initialize instance of <see cref="SolicitacoesController"/>
        /// </summary>
        /// <param name="gerenciamentoConta">Repositório de gerenciamento de contas</param>
        /// <param name="gerenciamentoAtendimento">Repositório de gerenciamento de atendimento</param>
        public SolicitacoesController(GerenciamentoConta gerenciamentoConta, GerenciamentoAtendimento gerenciamentoAtendimento)
        {
            _gerenciamentoConta = gerenciamentoConta;
            _gerenciamentoAtendimento = gerenciamentoAtendimento;
        }

        /// <summary>
        /// Obter todas as solicitações
        /// </summary>
        /// <remarks>
        /// Obtém todos as solicitações cadastrados
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("")]
        [ResponseType(typeof(IEnumerable<SolicitacaoDto>))]
        public IHttpActionResult ObterTodasSolicitacoes()
        {
            var solicitacoes = _gerenciamentoAtendimento.ObterTodasSolicitacoes();

            return Ok(solicitacoes);
        }

        /// <summary>
        /// Obter todas as solicitações pendentes
        /// </summary>
        /// <remarks>
        /// Obtém todos as solicitações pendentes
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("pendentes")]
        [ResponseType(typeof(IEnumerable<SolicitacaoDto>))]
        public IHttpActionResult ObterTodasSolicitacoesPendentes()
        {
            var solicitacoes = _gerenciamentoAtendimento.ObterTodasSolicitacoesPendentes();

            return Ok(solicitacoes);
        }

        /// <summary>
        /// Obter solicitação por id
        /// </summary>
        /// <remarks>
        /// Obtém as informações da solicitação solicitada
        /// </remarks>
        /// <param name="idSolicitacao">id da solicitação</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("{idSolicitacao}", Name = "ObterSolicitacaoPorId")]
        [ResponseType(typeof(SolicitacaoDto))]
        public IHttpActionResult ObterSolicitacaoPorId(int idSolicitacao)
        {
            var solicitacao = _gerenciamentoAtendimento.ObterSolicitacaoPorId(idSolicitacao);

            if (solicitacao == null)
                return NotFound();

            return Ok(solicitacao);
        }

        /// <summary>
        /// Altera uma solicitação existente
        /// </summary>
        /// <param name="idSolicitacao">Id da solicitação</param>
        /// <param name="solicitacaoParaAlterar">Informações da solicitação para serem alteradas</param>
        /// <response code="200">Ok</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut, Route("{idSolicitacao}")]
        [ResponseType(typeof(SolicitacaoDto))]
        public IHttpActionResult AlterarSolicitacao(int idSolicitacao, [FromBody]NovaSolicitacaoDto solicitacaoParaAlterar)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Exclui uma solicitação
        /// </summary>
        /// <param name="idSolicitacao">Id da solicitação</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpDelete, Route("{idSolicitacao}")]
        [ResponseType(typeof(SolicitacaoDto))]
        public IHttpActionResult ExcluirSolicitacao(int idSolicitacao)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Cria uma nova solicitação
        /// </summary>
        /// <param name="idConta">id da conta</param>
        /// <param name="novaSolicitacao">Informações da solicitação</param>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost, Route("~/api/v1/contas/{idConta}/solicitacoes")]
        [ResponseType(typeof(SolicitacaoDto))]
        public IHttpActionResult CriarSolicitacao(int idConta, [FromBody]NovaSolicitacaoDto novaSolicitacao)
        {
            var solicitacaoCriada = _gerenciamentoConta.AdicionarSolicitacao(idConta, novaSolicitacao);

            return CreatedAtRoute("ObterSolicitacaoPorId", new { idSolicitacao = solicitacaoCriada.CodigoSolicitacao }, solicitacaoCriada);
        }

        /// <summary>
        /// Obter todas as solicitações de uma conta
        /// </summary>
        /// <remarks>
        /// Obtém todas as solicitações de uma conta
        /// </remarks>
        /// <param name="idConta">id da conta</param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("~/api/v1/contas/{idConta}/solicitacoes")]
        [ResponseType(typeof(IEnumerable<SolicitacaoDto>))]
        public IHttpActionResult ObterSolicitacoesDaConta(int idConta)
        {
            var solicitacoes = _gerenciamentoAtendimento.ObterSolicitacoesDaConta(idConta);

            return Ok(solicitacoes);
        }

        /// <summary>
        /// Obter todas as solicitações pendentes de uma conta
        /// </summary>
        /// <remarks>
        /// Obtém todos as solicitações pendentes de uma conta
        /// </remarks>
        /// <param name="idConta">id da conta</param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("~/api/v1/contas/{idConta}/solicitacoes/pendentes")]
        [ResponseType(typeof(IEnumerable<SolicitacaoDto>))]
        public IHttpActionResult ObterSolicitacoesPendentesDaConta(int idConta)
        {
            var solicitacoes = _gerenciamentoAtendimento.ObterSolicitacoesPendentesDaConta(idConta);

            return Ok(solicitacoes);
        }
    }
}
