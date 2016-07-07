using System;
using System.Collections.Generic;
using CardapioDigital.Dominio.Conta;

namespace CardapioDigital.Aplicacao.DTO
{
    public class MesaDto
    {
        public int NumeroMesa { get; set; }
    }

    public class FechamentoContaDto
    {
        public int TipoDivisao { get; set; }
        public int FormaPagamento { get; set; }
        public bool IncuirDezPorcento { get; set; }
        public int QuantidadePessoas { get; set; }
    }

    public class ContaSimplesDto
    {
        public int CodigoConta { get; set; }
        public int NumeroMesa { get; set; }
        public string Situacao { get; set; }
        public string FormaPagamento { get; set; }
        public string TipoDivisao { get; set; }
        public DateTime DataCriacao { get; set; }
    }

    public class ContaDto
    {
        public ContaDto()
        {
            Pedido = new List<ItemPedidoDto>();
            Parciais = new List<ContaParcialDto>();
        }

        public int CodigoConta { get; set; }
        public int NumeroMesa { get; set; }
        public int CodigoSituacao { get; set; }
        public int? FormaPagamento { get; set; }
        public int? TipoDivisao { get; set; }
        public DateTime DataCriacao { get; set; }
        public AvaliacaoSimplesDto Avaliacao { get; set; }
        public IEnumerable<ItemPedidoDto> Pedido { get; set; }
        public IEnumerable<ContaParcialDto> Parciais { get; set; }
    }

    public class ContaParcialDto
    {
        public ContaParcialDto()
        {
            Itens = new List<ItemPedidoDto>();
        }

        public int CodigoContaParcial { get; set; }
        public string Cliente { get; set; }
        public int CodigoSituacao { get; set; }
        public int CodigoFormaPagamento { get; set; }
        public ICollection<ItemPedidoDto> Itens { get; set; }
    }

    public class ItemPedidoDto
    {
        public ItemPedidoDto()
        {
            this.ParciaisAssociadas = new List<ContaParcialDto>();
        }

        public int CodigoItemPedido { get; set; }
        public int CodigoSituacaoPreparo { get; set; }
        public ProdutoDto Produto { get; set; }
        public int DenominadorDivisao { get; set; }
        public IEnumerable<ContaParcialDto> ParciaisAssociadas { get; set; }
    }

    public class NovaSituacaoPreparoDto
    {
        public int CodigoSituacaoPreparo { get; set; }
    }

    public class NovoItemPedidoDto
    {
        public int CodigoProduto { get; set; }
        public int Quantidade { get; set; }
        public IEnumerable<int> CodigosOpcoesSelecionadas { get; set; }
    }

    public class NovoItemContaParcialDto
    {
        public int CodigoItemPedido { get; set; }
    }
    
    public class NovaContaParcial
    {
        public string NomeCliente { get; set; }
    }
}
