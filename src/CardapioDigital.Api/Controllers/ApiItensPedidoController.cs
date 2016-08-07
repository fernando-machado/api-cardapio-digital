using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Aplicacao.Servicos;

namespace CardapioDigital.Api.Controllers
{
    /// <summary>
    /// Pedidos Controller
    /// </summary>
    [RoutePrefix("api/v1/contas/{idConta}/pedidos/{idPedido}/itens")]
    public class ItensPedidoController : ApiController
    {
        private readonly GerenciamentoConta _gerenciamentoConta;

        /// <summary>
        /// Initialize instance of <see cref="ItensPedidoController"/>
        /// </summary>
        /// <param name="gerenciamentoConta">Repositório de gerenciamento de contas</param>
        public ItensPedidoController(GerenciamentoConta gerenciamentoConta)
        {
            _gerenciamentoConta = gerenciamentoConta;
        }

        /// <summary>
        /// Obter todos os pedidos da conta
        /// </summary>
        /// <remarks>
        /// Obtém todos os pedidos da conta informada
        /// </remarks>
        /// <param name="idConta">Id da conta</param>
        /// <param name="idPedido">Id do pedido</param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("")]
        [ResponseType(typeof(IEnumerable<ItemPedidoDto>))]
        public IHttpActionResult ObterTodosItensPedido(int idConta, int idPedido)
        {
            var itensPedidos = _gerenciamentoConta.ObterTodosItensPedido(idConta);

            return Ok(itensPedidos);
        }

        /// <summary>
        /// Obter item dp pedido por id
        /// </summary>
        /// <remarks>
        /// Obtém as informações do item do pedido solicitado
        /// </remarks>
        /// <param name="idConta">Id da conta</param>
        /// <param name="idPedido">Id do pedido</param>
        /// <param name="idItem">Id do item</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("{idItem:int}", Name = "ObterItemPedidoPorId")]
        [ResponseType(typeof(ItemPedidoDto))]
        public IHttpActionResult ObterItemPedidoPorId(int idConta, int idPedido, int idItem)
        {
            var pedido = _gerenciamentoConta.ObterItemPedido(idConta, idItem);

            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }

        /// <summary>
        /// Adiciona um novo item ao pedido
        /// </summary>
        /// <param name="idConta">Id da conta</param>
        /// <param name="idPedido">Id do pedido</param>
        /// <param name="novoItemPedido">Informações do novo item do pedido</param>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost, Route("")]
        [ResponseType(typeof(IEnumerable<ItemPedidoDto>))]
        public IHttpActionResult CriarItemPedido(int idConta, int idPedido, [FromBody]NovoItemPedidoDto novoItemPedido)
        {
            var itensPedido = _gerenciamentoConta.CriarPedido(idConta, novoItemPedido);

            //TODO: Refatorar
            //return CreatedAtRoute("ObterItemPedidoPorId", new { idConta, idPedido, idItem =  }, itensPedido);

            return CreatedAtRoute("ObterItemPedidoPorId", new { idConta, idPedido }, itensPedido);
        }

        /// <summary>
        /// Altera um item de um pedido existente
        /// </summary>
        /// <param name="idConta">Id da conta</param>
        /// <param name="idPedido">Id do pedido</param>
        /// <param name="idItem">Id do item</param>
        /// <param name="itemPedidoParaAlterar">Informações do item do pedidol para serem alteradas</param>
        /// <response code="200">Ok</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut, Route("{idItem:int}")]
        [ResponseType(typeof(ItemPedidoDto))]
        public IHttpActionResult AlterarItemPedido(int idConta, int idPedido, int idItem, [FromBody]NovaSituacaoPreparoDto itemPedidoParaAlterar)
        {
            var itemPedido = _gerenciamentoConta.AtualizarSituacaoPedido(idConta, idItem, itemPedidoParaAlterar);

            return Ok(itemPedido);
        }

        /// <summary>
        /// Exclui um item de um pedido
        /// </summary>
        /// <param name="idConta">Id da conta</param>
        /// <param name="idPedido">Id do pedido</param>
        /// <param name="idItem">Id do item</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpDelete, Route("{idItem:int}")]
        public IHttpActionResult ExcluirItemPedido(int idConta, int idPedido, int idItem)
        {
            throw new NotImplementedException();
            //_gerenciamentoConta.RemoverPedidoDaConta(idConta, idPedido);
        }
    }
}
