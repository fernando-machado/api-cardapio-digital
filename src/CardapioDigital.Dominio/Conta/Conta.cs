using System;
using System.Collections.Generic;
using System.Linq;
using CardapioDigital.Dominio.Atendimento;
using CardapioDigital.Dominio.Atendimento.Exceptions;
using CardapioDigital.Dominio.Conta.Exceptions;
using CardapioDigital.Dominio.Core;
using CardapioDigital.Dominio.Estoque;

namespace CardapioDigital.Dominio.Conta
{
    public class Conta : EntidadeBase
    {
        protected Conta()
        {
            _parciais = new List<ContaParcial>();
            _pedido = new List<ItemPedido>();
            _solicitacoes = new List<Solicitacao>();
        }

        public Conta(int numeroMesa)
            : this()
        {
            this.NumeroMesa = numeroMesa;
            this.DataCriacao = DateTime.Now;
            this.Situacao = SituacaoConta.Aberta;
        }

        public virtual int NumeroMesa { get; protected set; }
        public virtual DateTime DataCriacao { get; protected set; }
        public virtual SituacaoConta Situacao { get; protected set; }
        public virtual FormaPagamento? FormaPagamento { get; protected set; }
        public virtual TipoDivisaoConta? TipoDivisao { get; protected set; }
        public virtual Avaliacao Avaliacao { get; protected set; }

        private readonly ICollection<ContaParcial> _parciais;
        public virtual IEnumerable<ContaParcial> Parciais
        {
            get { return _parciais; }
        }

        private readonly ICollection<ItemPedido> _pedido;
        public virtual IEnumerable<ItemPedido> Pedido
        {
            get { return _pedido; }
        }

        private readonly ICollection<Solicitacao> _solicitacoes;
        public virtual IEnumerable<Solicitacao> Solicitacoes
        {
            get { return _solicitacoes; }
        }

        public virtual ItemPedido AdicionarItemPedido(Produto produto)
        {
            var itemPedido = new ItemPedido(produto);

            _pedido.Add(itemPedido);

            return itemPedido;
        }

        public virtual void RemoverItemPedido(ItemPedido itemPedido)
        {
            _pedido.Remove(itemPedido);
        }

        public virtual void RemoverItemPedido(int codigoItemPedido)
        {
            var itemPedido = _pedido.SingleOrDefault(ip => ip.Codigo == codigoItemPedido);
            if (itemPedido == null)
                throw new ItemPedidoNaoEncontradoException("Não foi possível encontrar o item do pedido solicitado. codigoItemPedido == {0}", codigoItemPedido);

            _pedido.Remove(itemPedido);
        }

        public virtual ContaParcial AdicionarContaParcial(string nomeCliente)
        {
            var contaParcial = new ContaParcial(nomeCliente);

            _parciais.Add(contaParcial);

            return contaParcial;
        }

        public virtual ContaParcial AdicionarContaParcial(ContaParcial contaParcial)
        {
            _parciais.Add(contaParcial);

            return contaParcial;
        }

        public virtual void RemoverContaParcial(int codigoContaParcial)
        {
            var contaParcial = _parciais.SingleOrDefault(ip => ip.Codigo == codigoContaParcial);
            if (contaParcial == null)
                throw new ContaParcialNaoEncontradaException("Não exite uma conta parcial com o código {0}", codigoContaParcial);

            if (contaParcial.Itens.Any())
                throw new InvalidOperationException(string.Format("Essa conta parcial possui itens associados. codigoContaParcial = {0}", codigoContaParcial));

            _parciais.Remove(contaParcial);
        }

        public virtual void Fechar(TipoDivisaoConta tipoDivisao, FormaPagamento formaPagamento, bool incuirDezPorcento, int quantidadePessoas = 0)
        {
            if (tipoDivisao == TipoDivisaoConta.PorConsumoIndividual)
            {
                var itensPedido = Pedido.ToList();
                var itensPedidoAssociadosContasParcias = Parciais.SelectMany(p => p.Itens).Distinct().ToList();

                if (itensPedido.Any(item => !itensPedidoAssociadosContasParcias.Contains(item)))
                    throw new FechamentoDeContaNaoPermitidoException("A conta {0} não pode ser fechada por divisão por consumo, pois ainda existem itens que não estão associados à parciais!", this.Codigo);
            }

            this.AdicionarSolicitacao(TipoSolicitacao.EncerrarConta, string.Format("A mesa {0} solicitou o fechamento da conta {1}", this.NumeroMesa, this.Codigo));

            this.TipoDivisao = tipoDivisao;
            this.FormaPagamento = formaPagamento;
            this.Situacao = SituacaoConta.Fechada;
        }

        public virtual void AvaliarAtendimento(byte notaGarcom, byte notaAtendimento, byte notaAmbiente, byte notaTempoAtendimento, byte notaCardapioTablet)
        {
            if (this.Avaliacao != null)
                throw new AvaliacaoDuplicadaException();

            this.Avaliacao = new Avaliacao(this, notaGarcom, notaAtendimento, notaAmbiente, notaTempoAtendimento, notaCardapioTablet);
        }

        public virtual Solicitacao AdicionarSolicitacao(TipoSolicitacao tipoSolicitacao, string mensagem)
        {
            var solicitacao = new Solicitacao(this, tipoSolicitacao, mensagem);

            this._solicitacoes.Add(solicitacao);

            return solicitacao;
        }
    }
}