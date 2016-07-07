using System;
using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Dominio.Atendimento;
using CardapioDigital.Dominio.Atendimento.Exceptions;
using CardapioDigital.Dominio.Conta;
using CardapioDigital.Dominio.Conta.Exceptions;
using CardapioDigital.Dominio.Core;
using System.Collections.Generic;
using System.Linq;
using CardapioDigital.Dominio.Estoque;
using CardapioDigital.Dominio.Estoque.Exceptions;

namespace CardapioDigital.Aplicacao.Servicos
{
    public class GerenciamentoConta
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IRepositorioDeContas _repositorioContas;
        private readonly IRepositorioDeOpcoes _repositorioOpcoes;
        private readonly IProdutos _produtos;

        public GerenciamentoConta(IUnidadeDeTrabalho unidadeDeTrabalho, IRepositorioDeContas contas, IRepositorioDeOpcoes opcoes, IProdutos produtos)
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _repositorioContas = contas;
            _repositorioOpcoes = opcoes;
            _produtos = produtos;
        }

        public IEnumerable<ContaDto> ObterTodasAsContas()
        {
            var contas = _repositorioContas.ObterTodos();

            return contas.Select(MapeamentoDtoHelper.MapContaSimplesParaDto);
        }

        public ContaDto ObterContaPorCodigo(int codigoConta)
        {
            var conta = ObterContaInterno(codigoConta);

            return MapeamentoDtoHelper.MapContaCompletaParaDto(conta);
        }

        public IEnumerable<ItemPedidoDto> ObterPedidos(int codigoConta)
        {
            var conta = ObterContaInterno(codigoConta);
            var itensPedidoConta = conta.Pedido.ToList();

            var itens = new List<ItemPedidoDto>();
            foreach (var itemPedido in itensPedidoConta)
            {
                var itemPedidoDto = MapeamentoDtoHelper.MapItemPedidoCompletoParaDto(itemPedido);
                var parciais = conta.Parciais.Where(p => p.Itens.Any(item => item == itemPedido)).ToList();
                if (parciais.Any())
                {
                    itemPedidoDto.ParciaisAssociadas = parciais.Select(MapeamentoDtoHelper.MapContaParcialSimplesParaDto);
                }

                itens.Add(itemPedidoDto);
            }

            return itens;
        }

        public ItemPedidoDto ObterItemPedido(int codigoConta, int codigoItemPedido)
        {
            var itemPedido = ObterItemPedidoInterno(codigoConta, codigoItemPedido);

            return MapeamentoDtoHelper.MapItemPedidoCompletoParaDto(itemPedido);
        }

        public IEnumerable<ContaParcialDto> ObterParciaisDaConta(int codigoConta)
        {
            var conta = ObterContaInterno(codigoConta);
            var contasParciais = conta.Parciais.ToList();
            var itens = contasParciais.SelectMany(cp => cp.Itens).ToList();

            var contasParciaisDtos = new List<ContaParcialDto>();
            foreach (var contaParcial in contasParciais)
            {
                var contaParcialDto = MapeamentoDtoHelper.MapContaParcialSimplesParaDto(contaParcial);

                foreach (var itemPedido in contaParcial.Itens.ToList())
                {
                    var itemPedidoDto = MapeamentoDtoHelper.MapItemPedidoCompletoParaDto(itemPedido);
                    itemPedidoDto.DenominadorDivisao = contasParciais.Sum(cp => cp.Itens.Count(i => i == itemPedido));

                    contaParcialDto.Itens.Add(itemPedidoDto);
                }

                contasParciaisDtos.Add(contaParcialDto);
            }

            return contasParciaisDtos;
        }

        public ContaParcialDto ObterContaParcial(int codigoConta, int codigoContaParcial)
        {
            var contaParcial = ObterContaParcialInterno(codigoConta, codigoContaParcial);

            return MapeamentoDtoHelper.MapContaParcialCompletaParaDto(contaParcial);
        }

        public ItemPedidoDto ObterItemDaContaParcial(int codigoConta, int codigoContaParcial, int codigoItemPedido)
        {
            var itemPedido = ObterItemPedidoContaParcialInterno(codigoConta, codigoContaParcial, codigoItemPedido);

            return MapeamentoDtoHelper.MapItemPedidoCompletoParaDto(itemPedido);
        }

        public IEnumerable<ItemPedidoDto> ObterItensDaContaParcial(int codigoConta, int codigoContaParcial)
        {
            var contaParcial = ObterContaParcialInterno(codigoConta, codigoContaParcial);

            return contaParcial.Itens.Select(MapeamentoDtoHelper.MapItemPedidoCompletoParaDto);
        }

        public AvaliacaoCompletaDto ObterAvaliacaoDaConta(int codigoConta)
        {
            var conta = ObterContaInterno(codigoConta);
            if (conta.Avaliacao == null)
                throw new AvaliacaoNaoEncontradaException("A conta {0} não possui uma avaliação.", codigoConta);

            return MapeamentoDtoHelper.MapAvaliacaoCompletaParaDto(conta.Avaliacao);
        }




        public ContaDto CriarConta(int numeroMesa)
        {
            var conta = new Conta(numeroMesa);

            _repositorioContas.Salvar(conta);

            _unidadeDeTrabalho.Commit();

            return MapeamentoDtoHelper.MapContaSimplesParaDto(conta);
        }

        public IEnumerable<ItemPedidoDto> CriarPedido(int codigoConta, NovoItemPedidoDto novoItemPedido)
        {
            var conta = ObterContaInterno(codigoConta);
            var opcoes = ObterOpcoesInterno(novoItemPedido.CodigosOpcoesSelecionadas);
            var produto = ObterProdutoInterno(novoItemPedido.CodigoProduto);

            for (var i = 0; i < novoItemPedido.Quantidade; i++)
            {
                var itemPedido = conta.AdicionarItemPedido(produto);

                foreach (var opcao in opcoes)
                    itemPedido.AdicionarOpcao(opcao);
            }

            _repositorioContas.Salvar(conta);
            _unidadeDeTrabalho.Commit();

            return conta.Pedido.ToList().Select(MapeamentoDtoHelper.MapItemPedidoCompletoParaDto);
        }

        public ContaParcialDto CriarContaParcial(int codigoConta, NovaContaParcial novaContaParcial)
        {
            var conta = ObterContaInterno(codigoConta);

            var contaParcial = conta.AdicionarContaParcial(novaContaParcial.NomeCliente);

            _repositorioContas.Salvar(conta);
            _unidadeDeTrabalho.Commit();

            return MapeamentoDtoHelper.MapContaParcialCompletaParaDto(contaParcial);
        }

        public ItemPedidoDto CriarItemNaContaParciai(int codigoConta, int codigoContaParcial, NovoItemContaParcialDto novoItemContaParcial)
        {
            var conta = ObterContaInterno(codigoConta);
            var contaParcial = ObterContaParcialInterno(conta, codigoContaParcial);

            var itemPedido = conta.Pedido.SingleOrDefault(ip => ip.Codigo == novoItemContaParcial.CodigoItemPedido);

            contaParcial.AdicionarItemPedido(itemPedido);

            _repositorioContas.Salvar(conta);
            _unidadeDeTrabalho.Commit();

            return MapeamentoDtoHelper.MapItemPedidoCompletoParaDto(itemPedido);
        }

        public void RemoverContaParcial(int codigoConta, int codigoContaParcial)
        {
            var conta = ObterContaInterno(codigoConta);

            conta.RemoverContaParcial(codigoContaParcial);

            _unidadeDeTrabalho.Commit();
        }

        public void RemoverItemContaParcial(int codigoConta, int codigoContaParcial, int codigoItemPedido)
        {
            var contaParcial = ObterContaParcialInterno(codigoConta, codigoContaParcial);

            contaParcial.RemoverItemPedido(codigoItemPedido);

            _unidadeDeTrabalho.Commit();
        }

        public void SalvarAvaliacaoAtendimento(int codigoConta, NovaAvaliacaoDto avaliacaoConta)
        {
            var conta = ObterContaInterno(codigoConta);

            conta.AvaliarAtendimento(
                    avaliacaoConta.NotaGarcom, 
                    avaliacaoConta.NotaAtendimento, 
                    avaliacaoConta.NotaAmbiente, 
                    avaliacaoConta.NotaTempoAtendimento, 
                    avaliacaoConta.NotaCardapioTablet);
        
            _repositorioContas.Salvar(conta);
            _unidadeDeTrabalho.Commit();
        }

        public SolicitacaoDto AdicionarSolicitacao(int codigoConta, NovaSolicitacaoDto novaSolicitacao)
        {
            var conta = ObterContaInterno(codigoConta);

            var tipoSolicitacao = (TipoSolicitacao?)novaSolicitacao.TipoSolicitacao;
            if (!tipoSolicitacao.HasValue)
                throw new InvalidOperationException("O tipo de solicitação deve ser informado!");

            var mensagem = novaSolicitacao.Mensagem;
            if(string.IsNullOrWhiteSpace(mensagem))
                throw new InvalidOperationException("A mensagem da solicitação deve ser informada!");

            var solicitacao = conta.AdicionarSolicitacao(tipoSolicitacao.Value, mensagem);

            _repositorioContas.Salvar(conta);
            _unidadeDeTrabalho.Commit();

            return MapeamentoDtoHelper.MapSolicitacaoCompletaParaDto(solicitacao);
        }



        public void FecharConta(int codigoConta, FechamentoContaDto dadosFechamentoConta)
        {
            var conta = ObterContaInterno(codigoConta);

            var tipoDivisaoConta = (TipoDivisaoConta?)dadosFechamentoConta.TipoDivisao;
            if (!tipoDivisaoConta.HasValue)
                throw new InvalidOperationException("O tipo de divisão da conta deve ser informado!");

            var formaPagamento = (FormaPagamento?)dadosFechamentoConta.FormaPagamento;
            if (!formaPagamento.HasValue)
                throw new InvalidOperationException("A forma de pagamento da conta deve ser informado!");
            
            conta.Fechar(tipoDivisaoConta.Value, formaPagamento.Value, dadosFechamentoConta.IncuirDezPorcento, dadosFechamentoConta.QuantidadePessoas);

            _repositorioContas.Salvar(conta);

            _unidadeDeTrabalho.Commit();
        }

        public void AtualizarSituacaoPedido(int codigoConta, int codigoItemPedido, NovaSituacaoPreparoDto novaSituacaoPreparo)
        {
            var itemPedido = ObterItemPedidoInterno(codigoConta, codigoItemPedido);

            var situacaoPreparo = (SituacaoPreparo?)novaSituacaoPreparo.CodigoSituacaoPreparo;
            if (!situacaoPreparo.HasValue)
                throw new SituacaoPreparoException("Não existe uma situação de preparo com o código {0}", novaSituacaoPreparo.CodigoSituacaoPreparo);

            itemPedido.AtualizarSituacaoPreparo(situacaoPreparo.Value);

            _unidadeDeTrabalho.Commit();
        }


        private Conta ObterContaInterno(int codigoConta)
        {
            var conta = _repositorioContas.ObterPorId(codigoConta);
            if (conta == null)
                throw new ContaNaoEncontradaException("Não exite uma conta com o código {0}", codigoConta);

            return conta;
        }

        private ItemPedido ObterItemPedidoInterno(int codigoConta, int codigoItemPedido)
        {
            var conta = ObterContaInterno(codigoConta);

            var itemPedido = conta.Pedido.SingleOrDefault(ip => ip.Codigo == codigoItemPedido);
            if (itemPedido == null)
                throw new ItemPedidoNaoEncontradoException("Não exite um item com o código {0}", codigoItemPedido);

            return itemPedido;
        }

        private Produto ObterProdutoInterno(int codigoProduto)
        {
            var produto = _produtos.ObterPorId(codigoProduto);
            if (produto == null)
                throw new ProdutoNaoEncontradoException("Não exite um produto com o código {0}", codigoProduto);

            return produto;
        }

        private ContaParcial ObterContaParcialInterno(int codigoConta, int codigoContaParcial)
        {
            var conta = ObterContaInterno(codigoConta);

            return ObterContaParcialInterno(conta, codigoContaParcial);
        }

        private static ContaParcial ObterContaParcialInterno(Conta conta, int codigoContaParcial)
        {
            var contaParcial = conta.Parciais.SingleOrDefault(ip => ip.Codigo == codigoContaParcial);
            if (contaParcial == null)
                throw new ContaParcialNaoEncontradaException("Não exite uma conta parcial com o código {0}", codigoContaParcial);

            return contaParcial;
        }

        private ItemPedido ObterItemPedidoContaParcialInterno(int codigoConta, int codigoContaParcial, int codigoItemPedido)
        {
            var contaParcial = ObterContaParcialInterno(codigoConta, codigoContaParcial);

            var itemPedido = contaParcial.Itens.SingleOrDefault(i => i.Codigo == codigoItemPedido);
            if (itemPedido == null)
                throw new ItemPedidoNaoEncontradoException("Não foi possível encontrar o item do pedido solicitado! codigoItemPedido == {0}", codigoItemPedido);

            return itemPedido;
        }

        private IEnumerable<Opcao> ObterOpcoesInterno(IEnumerable<int> codigosOpcoes)
        {
            if (codigosOpcoes == null || !codigosOpcoes.Any())
                return new List<Opcao>();

            var opcoes = _repositorioOpcoes.ObterTodosOnde(op => codigosOpcoes.Contains(op.Codigo));
            if (!opcoes.Any())
                throw new ProdutoNaoEncontradoException("Não exitem opções com os códigos {0}", string.Join(",", codigosOpcoes));

            return opcoes;
        }
    }
}
