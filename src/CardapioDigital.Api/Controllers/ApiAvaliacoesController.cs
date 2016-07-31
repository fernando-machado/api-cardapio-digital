using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Aplicacao.Servicos;

namespace CardapioDigital.Api.Controllers
{
    /// <summary>
    /// Avaliacoes Controller
    /// </summary>
    [RoutePrefix("api/v1/avaliacoes")]
    public class AvaliacoesController : ApiController
    {
        private readonly GerenciamentoConta _gerenciamentoConta;
        private readonly GerenciamentoAtendimento _gerenciamentoAtendimento;

        /// <summary>
        /// Initialize instance of <see cref="AvaliacoesController"/>
        /// </summary>
        /// <param name="gerenciamentoConta">Repositório de gerenciamento de contas</param>
        /// <param name="gerenciamentoAtendimento">Repositório de gerenciamento de atendimento</param>
        public AvaliacoesController(GerenciamentoConta gerenciamentoConta, GerenciamentoAtendimento gerenciamentoAtendimento)
        {
            _gerenciamentoConta = gerenciamentoConta;
            _gerenciamentoAtendimento = gerenciamentoAtendimento;
        }

        /// <summary>
        /// Obter todas as avaliações
        /// </summary>
        /// <remarks>
        /// Obtém todos as avaliações cadastrados
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("")]
        [ResponseType(typeof(IEnumerable<AvaliacaoCompletaDto>))]
        public IHttpActionResult ObterTodasAvaliacoes()
        {
            var avaliacoes = _gerenciamentoAtendimento.ObterTodasAvaliacoes();

            return Ok(avaliacoes);
        }

        /// <summary>
        /// Obter avaliações por data
        /// </summary>
        /// <remarks>
        /// Obtém as informações das avaliações da data solicitada
        /// </remarks>
        /// <param name="data">data das avaliações</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("{data:datetime}")]
        [ResponseType(typeof(IEnumerable<AvaliacaoCompletaDto>))]
        public IHttpActionResult ObterAvaliacaoPorData(DateTime data)
        {
            var avaliacoes = _gerenciamentoAtendimento.ObterAvaliacoesPorData(data);

            return Ok(avaliacoes);
        }

        /// <summary>
        /// Obter avaliação por id
        /// </summary>
        /// <remarks>
        /// Obtém as informações da avaliação solicitada
        /// </remarks>
        /// <param name="idAvaliacao">id da avaliação</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("{idAvaliacao:int}", Name = "ObterAvaliacaoPorId")]
        [ResponseType(typeof(AvaliacaoCompletaDto))]
        public IHttpActionResult ObterAvaliacaoPorId(int idAvaliacao)
        {
            var avaliacao = _gerenciamentoAtendimento.ObterAvaliacaoPorId(idAvaliacao);

            if (avaliacao == null)
                return NotFound();

            return Ok(avaliacao);
        }

        /// <summary>
        /// Obter avaliação de uma conta
        /// </summary>
        /// <remarks>
        /// Obtém as informações da avaliação de uma conta específica
        /// </remarks>
        /// <param name="idConta">id da conta</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("~/api/v1/contas/{idConta:int}/avaliacoes")]
        [ResponseType(typeof(AvaliacaoCompletaDto))]
        public IHttpActionResult ObterAvaliacaoDaConta(int idConta)
        {
            var avaliacao = _gerenciamentoConta.ObterAvaliacaoDaConta(idConta);

            if (avaliacao == null)
                return NotFound();

            return Ok(avaliacao);
        }

        /// <summary>
        /// Cria uma nova avaliação
        /// </summary>
        /// <param name="idConta">id da conta</param>
        /// <param name="novaAvaliacao">Informações da nova avaliação</param>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost, Route("~/api/v1/contas/{idConta:int}/avaliacoes")]
        [ResponseType(typeof(AvaliacaoCompletaDto))]
        public IHttpActionResult CriarAvaliacao(int idConta, [FromBody]NovaAvaliacaoDto novaAvaliacao)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var avaliacao = _gerenciamentoConta.SalvarAvaliacaoAtendimento(idConta, novaAvaliacao);

            return CreatedAtRoute("ObterAvaliacaoPorId", new { idAvaliacao = avaliacao.Codigo }, avaliacao);
        }
    }
}
