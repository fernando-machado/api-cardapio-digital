using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Aplicacao.Servicos;

namespace CardapioDigital.Api.Controllers
{
    /// <summary>
    /// Itens Conta Parcial Controller
    /// </summary>
    [RoutePrefix("api/v1/parciais/{idParcial}/itens")]
    public class ItensContaParcialController : ApiController
    {
        private readonly GerenciamentoConta _gerenciamentoConta;

        /// <summary>
        /// Initialize instance of <see cref="ItensContaParcialController"/>
        /// </summary>
        /// <param name="gerenciamentoConta">Repositório de gerenciamento de contas</param>
        public ItensContaParcialController(GerenciamentoConta gerenciamentoConta)
        {
            _gerenciamentoConta = gerenciamentoConta;
        }

        /// <summary>
        /// Obter todos os itens das contas parciais
        /// </summary>
        /// <remarks>
        /// Obtém todos os itens das contas parciais cadastradas
        /// </remarks>
        /// <param name="idParcial">Id da conta parcial</param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("")]
        [ResponseType(typeof(IEnumerable<ItemPedidoDto>))]
        public IHttpActionResult ObterTodosItensContaParcial(int idParcial)
        {
            var itensContaParcial = _gerenciamentoConta.ObterItensDaContaParcial(idParcial);

            return Ok(itensContaParcial);
        }

        /// <summary>
        /// Obter item da conta parcial por id
        /// </summary>
        /// <remarks>
        /// Obtém as informações do item da conta parcial solicitado
        /// </remarks>
        /// <param name="idParcial">Id da conta parcial</param>
        /// <param name="idItem">Id do item da conta parcial</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet, Route("{idItem:int}", Name = "ObterItemContaParcialPorId")]
        [ResponseType(typeof(ItemPedidoDto))]
        public IHttpActionResult ObterItemContaParcialPorId(int idParcial, int idItem)
        {
            var itemContaParcial = _gerenciamentoConta.ObterItemDaContaParcial(idParcial, idItem);

            if (itemContaParcial == null) 
                return NotFound();

            return Ok(itemContaParcial);
        }

        /// <summary>
        /// Adiciona um novo item à conta parcial
        /// </summary>
        /// <param name="idParcial">Id da conta parcial</param>
        /// <param name="novoItemContaParcial">Informações do novo item da conta parcial</param>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost, Route("")]
        [ResponseType(typeof(ItemPedidoDto))]
        public IHttpActionResult CriarItemContaParcial(int idParcial, [FromBody]NovoItemContaParcialDto novoItemContaParcial)
        {
            var itemContaParcialCriado = _gerenciamentoConta.CriarItemNaContaParcial(idParcial, novoItemContaParcial);

            return CreatedAtRoute("ObterItemContaParcialPorId", new { idParcial, idItem = itemContaParcialCriado.CodigoItemPedido }, itemContaParcialCriado);
        }

        /// <summary>
        /// Altera um item de uma conta parcial existente
        /// </summary>
        /// <param name="idParcial">Id da conta parcial</param>
        /// <param name="idItem">Id do item da conta parcial</param>
        /// <param name="itemContaParcialParaAtualizar">Informações do item da conta parcial para serem alteradas</param>
        /// <response code="200">Ok</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut, Route("{idItem:int}")]
        [ResponseType(typeof(ItemPedidoDto))]
        public IHttpActionResult AlterarItemContaParcial(int idParcial, int idItem, [FromBody]ItemPedidoDto itemContaParcialParaAtualizar)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Exclui um item de uma conta parcial
        /// </summary>
        /// <param name="idParcial">Id da conta parcial</param>
        /// <param name="idItem">Id do item da conta parcial</param>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        [HttpDelete, Route("{idItem:int}")]
        public IHttpActionResult ExcluirItemContaParcial(int idParcial, int idItem)
        {
            _gerenciamentoConta.RemoverItemContaParcial(idParcial, idItem);

            return Ok();
        }
    }
}
