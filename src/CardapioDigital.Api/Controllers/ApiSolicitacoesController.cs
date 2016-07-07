using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Aplicacao.Servicos;

namespace CardapioDigital.Api.Controllers
{
    public class SolicitacoesController : ApiController
    {
        private readonly GerenciamentoConta _gerenciamentoConta;
        private readonly GerenciamentoAtendimento _gerenciamentoAtendimento;

        public SolicitacoesController(GerenciamentoConta gerenciamentoConta, GerenciamentoAtendimento gerenciamentoAtendimento)
        {
            _gerenciamentoConta = gerenciamentoConta;
            _gerenciamentoAtendimento = gerenciamentoAtendimento;
        }

        // GET api/solicitacoes
        public IEnumerable<SolicitacaoDto> Get()
        {
            return _gerenciamentoAtendimento.ObterTodasSolicitacoes();
        }

        // GET api/solicitacoes/{idConta}
        // GET api/contas/{idConta}/solicitacoes
        public IEnumerable<SolicitacaoDto> Get(int idConta)
        {
            return _gerenciamentoAtendimento.ObterSolicitacoesDaConta(idConta);
        }

        // GET api/solicitacoes/{idSolicitacao}
        // GET api/contas/{idConta}/solicitacoes/{idSolicitacao}
        public SolicitacaoDto Get(int idConta, int idSolicitacao)
        {
            return _gerenciamentoAtendimento.ObterSolicitacao(idSolicitacao);
        }

        // POST api/solicitacoes
        // POST api/contas/{idConta}/solicitacoes
        public SolicitacaoDto Post(int idConta, [FromBody]NovaSolicitacaoDto novaSolicitacao)
        {
            return _gerenciamentoConta.AdicionarSolicitacao(idConta, novaSolicitacao);
        }

        // PUT api/solicitacoes/{idSolicitacao}
        // PUT api/contas/{idConta}/solicitacoes/{idSolicitacao}
        public void Put(int idConta, int idSolicitacao, [FromBody]NovaSolicitacaoDto solicitacao)
        {
            throw new HttpResponseException(HttpStatusCode.NotImplemented);
        }

        // DELETE api/solicitacoes/{idSolicitacao}
        // DELETE api/contas/{idConta}/solicitacoes/{idSolicitacao}
        public void Delete(int idConta, int idSolicitacao)
        {
            throw new HttpResponseException(HttpStatusCode.NotImplemented);
        }
    }
}
