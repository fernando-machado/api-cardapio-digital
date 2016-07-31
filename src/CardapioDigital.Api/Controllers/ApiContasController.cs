using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Aplicacao.Servicos;

namespace CardapioDigital.Api.Controllers
{
    /// <summary>
    /// Contas Controller
    /// </summary>
    [RoutePrefix()]
    public class ContasController : ApiController
    {
        private readonly GerenciamentoConta _gerenciamentoConta;

        public ContasController(GerenciamentoConta gerenciamentoConta)
        {
            _gerenciamentoConta = gerenciamentoConta;
        }


        // GET api/contas
        public IEnumerable<ContaDto> Get()
        {
            return _gerenciamentoConta.ObterTodasAsContas();
        }

        // GET api/contas/{idConta}
        public ContaDto Get(int idConta)
        {
            return _gerenciamentoConta.ObterContaPorCodigo(idConta);
        }

        // POST api/contas
        public ContaDto Post([FromBody] MesaDto mesa)
        {
            return _gerenciamentoConta.CriarConta(mesa.NumeroMesa);
        }

        // PUT api/contas/{idConta}
        public void Put(int idConta, [FromBody]FechamentoContaDto dadosConta)
        {
            _gerenciamentoConta.FecharConta(idConta, dadosConta);
        }

        // DELETE api/contas/{idConta}
        public void Delete(int idConta)
        {
            throw new HttpResponseException(HttpStatusCode.NotImplemented);
        }
    }
}
