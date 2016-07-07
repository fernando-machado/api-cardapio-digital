using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Aplicacao.Servicos;

namespace CardapioDigital.Api.Controllers
{
    public class PedidosController : ApiController
    {
        private readonly GerenciamentoConta _gerenciamentoConta;

        public PedidosController(GerenciamentoConta gerenciamentoConta)
        {
            _gerenciamentoConta = gerenciamentoConta;
        }


        // GET api/contas/{idConta}/pedidos
        public IEnumerable<ItemPedidoDto> Get(int idConta)
        {
            return _gerenciamentoConta.ObterPedidos(idConta);
        }

        // GET api/contas/{idConta}/pedidos/{idPedido}
        public ItemPedidoDto Get(int idConta, int idPedido)
        {
            return _gerenciamentoConta.ObterItemPedido(idConta, idPedido);
        }

        // POST api/contas/{idConta}/pedidos
        public IEnumerable<ItemPedidoDto> Post(int idConta, [FromBody]NovoItemPedidoDto novoItemPedido)
        {
            return _gerenciamentoConta.CriarPedido(idConta, novoItemPedido);
        }

        // PUT api/contas/{idConta}/pedidos/{idPedido}
        public void Put(int idConta, int idPedido, [FromBody]NovaSituacaoPreparoDto novaSituacaoPreparo)
        {
            _gerenciamentoConta.AtualizarSituacaoPedido(idConta, idPedido, novaSituacaoPreparo);
        }

        // DELETE api/contas/{idConta}/pedidos/{idPedido}
        public void Delete(int idConta, int idPedido)
        {
            throw new HttpResponseException(HttpStatusCode.NotImplemented);
            //_gerenciamentoConta.RemoverPedidoDaConta(idConta, idPedido);
        }
    }
}
