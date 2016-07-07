using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Aplicacao.Servicos;

namespace CardapioDigital.Api.Controllers
{
    public class AvaliacoesController : ApiController
    {
        private readonly GerenciamentoConta _gerenciamentoConta;
        private readonly GerenciamentoAtendimento _gerenciamentoAtendimento;

        public AvaliacoesController(GerenciamentoConta gerenciamentoConta, GerenciamentoAtendimento gerenciamentoAtendimento)
        {
            _gerenciamentoConta = gerenciamentoConta;
            _gerenciamentoAtendimento = gerenciamentoAtendimento;
        }

        // GET api/avaliacoes
        public IEnumerable<AvaliacaoCompletaDto> Get()
        {
            return _gerenciamentoAtendimento.ObterTodasAvaliacoes();
        }

        // GET api/avaliacoes/{idConta}
        // GET api/contas/{idConta}/avaliacoes
        public IEnumerable<AvaliacaoCompletaDto> Get(int idConta)
        {
            return new[] { _gerenciamentoConta.ObterAvaliacaoDaConta(idConta) };
        }

        // GET api/avaliacoes/{idAvaliacao}
        // GET api/contas/{idConta}/avaliacoes/{idAvaliacao}
        public AvaliacaoCompletaDto Get(int idConta, int idAvaliacao)
        {
            return _gerenciamentoConta.ObterAvaliacaoDaConta(idConta);
        }

        // POST api/avaliacoes
        // POST api/contas/{idConta}/avaliacoes
        public void Post(int idConta, [FromBody]NovaAvaliacaoDto novaAvaliacao)
        {
            _gerenciamentoConta.SalvarAvaliacaoAtendimento(idConta, novaAvaliacao);
        }

        // PUT api/avaliacoes/{idAvaliacao}
        // PUT api/contas/{idConta}/avaliacoes/{idAvaliacao}
        public void Put(int idConta, int idAvaliacao, [FromBody]NovaAvaliacaoDto avaliacaoDto)
        {
            throw new HttpResponseException(HttpStatusCode.NotImplemented);
        }

        // DELETE api/avaliacoes/{idAvaliacao}
        // DELETE api/contas/{idConta}/avaliacoes/{idAvaliacao}
        public void Delete(int idConta, int idAvaliacao)
        {
            throw new HttpResponseException(HttpStatusCode.NotImplemented);
        }
    }
}
