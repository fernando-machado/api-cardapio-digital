using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Aplicacao.Servicos;

namespace CardapioDigital.Api.Controllers
{
    public class ItensController : ApiController
    {
        private readonly GerenciamentoConta _gerenciamentoConta;

        public ItensController(GerenciamentoConta gerenciamentoConta)
        {
            _gerenciamentoConta = gerenciamentoConta;
        }


        // GET api/contas/{idConta}/parciais/{idContaParcial}/itens
        public IEnumerable<ItemPedidoDto> Get(int idConta, int idContaParcial)
        {
            return _gerenciamentoConta.ObterItensDaContaParcial(idConta, idContaParcial);
        }

        // GET api/contas/{idConta}/parciais/{idContaParcial}/itens/{idItem}
        public ItemPedidoDto Get(int idConta, int idContaParcial, int idItem)
        {
            return _gerenciamentoConta.ObterItemDaContaParcial(idConta, idContaParcial, idItem);
        }

        // POST api/contas/{idConta}/parciais/{idContaParcial}/itens
        public ItemPedidoDto Post(int idConta, int idContaParcial, [FromBody]NovoItemContaParcialDto novoItemPedido)
        {
            return _gerenciamentoConta.CriarItemNaContaParciai(idConta, idContaParcial, novoItemPedido);
        }

        // PUT api/contas/{idConta}/parciais/{idContaParcial}/itens/{idItem}
        public void Put(int idConta, int idContaParcial, int idItem, [FromBody]ItemPedidoDto dadosItemPedido)
        {
            throw new HttpResponseException(HttpStatusCode.NotImplemented);
        }

        // DELETE api/contas/{idConta}/parciais/{idContaParcial}/itens/{idItem}
        public void Delete(int idConta, int idContaParcial, int idItem)
        {
            _gerenciamentoConta.RemoverItemContaParcial(idConta, idContaParcial, idItem);
        }
    }
}
