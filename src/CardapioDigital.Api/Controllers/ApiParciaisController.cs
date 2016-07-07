using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Aplicacao.Servicos;

namespace CardapioDigital.Api.Controllers
{
    public class ParciaisController : ApiController
    {
        private readonly GerenciamentoConta _gerenciamentoConta;

        public ParciaisController(GerenciamentoConta gerenciamentoConta)
        {
            _gerenciamentoConta = gerenciamentoConta;
        }


        // GET api/contas/{idConta}/parciais
        public IEnumerable<ContaParcialDto> Get(int idConta)
        {
            return _gerenciamentoConta.ObterParciaisDaConta(idConta);
        }

        // GET api/contas/{idConta}/parciais/{idContaParcial}
        public ContaParcialDto Get(int idConta, int idContaParcial)
        {
            return _gerenciamentoConta.ObterContaParcial(idConta, idContaParcial);
        }

        // POST api/contas/{idConta}/parciais
        public ContaParcialDto Post(int idConta, [FromBody]NovaContaParcial novaContaParcial)
        {
            return _gerenciamentoConta.CriarContaParcial(idConta, novaContaParcial);
        }

        // PUT api/contas/{idConta}/parciais/{idContaParcial}
        public void Put(int idConta, int idContaParcial, [FromBody]ContaParcialDto dadosContaParcial)
        {
            throw new HttpResponseException(HttpStatusCode.NotImplemented);
        }

        // DELETE api/contas/{idConta}/parciais/{idContaParcial}
        public void Delete(int idConta, int idContaParcial)
        {
            _gerenciamentoConta.RemoverContaParcial(idConta, idContaParcial);
        }
    }
}
