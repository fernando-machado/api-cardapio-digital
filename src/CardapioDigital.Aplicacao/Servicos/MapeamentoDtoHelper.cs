using System.Linq;
using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Dominio.Atendimento;
using CardapioDigital.Dominio.Conta;
using CardapioDigital.Dominio.Estoque;

namespace CardapioDigital.Aplicacao.Servicos
{
    public class MapeamentoDtoHelper
    {
        #region Estoque
        public static CategoriaDto MapCategoriaSimplesParaDto(Categoria categoria)
        {
            return new CategoriaDto
            {
                Codigo = categoria.Codigo,
                Nome = categoria.TraducaoCorrente.Nome,
                Descricao = categoria.TraducaoCorrente.Descricao
            };
        }

        public static CategoriaDto MapCategoriaCompletaParaDto(Categoria categoria)
        {
            var categoriaDto = MapCategoriaSimplesParaDto(categoria);

            categoriaDto.Subcategorias = categoria.Subcategorias.ToList().Select(MapSubcategoriaCompletaParaDto);

            return categoriaDto;
        }

        public static SubcategoriaDto MapSubcategoriaSimplesParaDto(Subcategoria subcategoria)
        {
            return new SubcategoriaDto
            {
                Codigo = subcategoria.Codigo,
                Nome = subcategoria.TraducaoCorrente.Nome,
                Descricao = subcategoria.TraducaoCorrente.Descricao
            };
        }

        public static SubcategoriaDto MapSubcategoriaCompletaParaDto(Subcategoria subcategoria)
        {
            var subcategoriaDto = MapSubcategoriaSimplesParaDto(subcategoria);

            subcategoriaDto.Produtos = subcategoria.Produtos.ToList().Select(MapProdutoCompletoParaDto);

            return subcategoriaDto;
        }

        public static ProdutoDto MapProdutoSimplesParaDto(Produto produto)
        {
            return new ProdutoDto
            {
                Codigo = produto.Codigo,
                Nome = produto.TraducaoCorrente.Nome,
                Descricao = produto.TraducaoCorrente.Descricao,
                Preco = produto.Preco,
                Imagem = produto.Imagem,
                Destaque = produto.Destaque
            };
        }

        public static ProdutoDto MapProdutoCompletoParaDto(Produto produto)
        {
            var produtoDto = MapProdutoSimplesParaDto(produto);
            produtoDto.Opcoes = produto.Opcoes.ToList().Select(MapOpcaoParaDto);

            return produtoDto;
        }

        public static OpcaoDto MapOpcaoParaDto(Opcao opcao)
        {
            return new OpcaoDto
            {
                Codigo = opcao.Codigo,
                Nome = opcao.TraducaoCorrente.Nome,
                Descricao = opcao.TraducaoCorrente.Descricao
            };
        }
        #endregion Estoque

        
        #region Conta
        public static ContaDto MapContaSimplesParaDto(Conta conta)
        {
            return new ContaDto
            {
                CodigoConta = conta.Codigo,
                NumeroMesa = conta.NumeroMesa,
                CodigoSituacao = (int)conta.Situacao,
                FormaPagamento = (int?)conta.FormaPagamento,
                TipoDivisao = (int?)conta.TipoDivisao,
                DataCriacao = conta.DataCriacao
            };
        }

        public static ContaDto MapContaCompletaParaDto(Conta conta)
        {
            var contaDto = MapContaSimplesParaDto(conta);

            contaDto.Avaliacao = MapAvaliacaoSimplesParaDto(conta.Avaliacao);
            contaDto.Pedido = conta.Pedido.ToList().Select(MapItemPedidoCompletoParaDto);
            contaDto.Parciais = conta.Parciais.ToList().Select(MapContaParcialSimplesParaDto);

            return contaDto;
        }

        public static ContaParcialDto MapContaParcialSimplesParaDto(ContaParcial contaParcial)
        {
            return new ContaParcialDto
            {
                CodigoContaParcial = contaParcial.Codigo,
                Cliente = contaParcial.Cliente
            };
        }

        public static ContaParcialDto MapContaParcialCompletaParaDto(ContaParcial contaParcial)
        {
            var contaParcialDto = MapContaParcialSimplesParaDto(contaParcial);

            contaParcialDto.Itens = contaParcial.Itens.ToList().Select(MapItemPedidoCompletoParaDto).ToList();

            return contaParcialDto;
        }

        public static ItemPedidoDto MapItemPedidoSimplesParaDto(ItemPedido itemPedido)
        {
            return new ItemPedidoDto
            {
                CodigoItemPedido = itemPedido.Codigo,
                CodigoSituacaoPreparo = (int)itemPedido.SituacaoPreparo
            };
        }

        public static ItemPedidoDto MapItemPedidoCompletoParaDto(ItemPedido itemPedido)
        {
            var itemPedidoDto = MapItemPedidoSimplesParaDto(itemPedido);

            itemPedidoDto.Produto = MapProdutoCompletoParaDto(itemPedido.Produto);

            var opcoesDto = itemPedidoDto.Produto.Opcoes.ToList();
            foreach (var opcaoDto in opcoesDto)
            {
                opcaoDto.Selecionada = itemPedido.OpcoesSelecionadas.Any(op => op.Codigo == opcaoDto.Codigo);
            }

            itemPedidoDto.Produto.Opcoes = opcoesDto;

            return itemPedidoDto;
        }

        private static AvaliacaoSimplesDto MapAvaliacaoSimplesParaDto(Avaliacao avaliacao)
        {
            if (avaliacao == null)
                return null;

            return new AvaliacaoSimplesDto
            {
                CodigoAvaliacao = avaliacao.Codigo,
                NotaGarcom = avaliacao.NotaGarcom,
                NotaAtendimento = avaliacao.NotaAtendimento,
                NotaAmbiente = avaliacao.NotaAmbiente,
                NotaTempoAtendimento = avaliacao.NotaTempoAtendimento,
                NotaCardapioTablet = avaliacao.NotaCardapioTablet
            };
        }

        public static AvaliacaoCompletaDto MapAvaliacaoCompletaParaDto(Avaliacao avaliacao)
        {
            var conta = avaliacao.Conta;

            return new AvaliacaoCompletaDto
            {
                CodigoAvaliacao = avaliacao.Codigo,
                NotaGarcom = avaliacao.NotaGarcom,
                NotaAtendimento = avaliacao.NotaAtendimento,
                NotaAmbiente = avaliacao.NotaAmbiente,
                NotaTempoAtendimento = avaliacao.NotaTempoAtendimento,
                NotaCardapioTablet = avaliacao.NotaCardapioTablet,
                Conta = new ContaSimplesDto
                {
                    CodigoConta = conta.Codigo,
                    Situacao = conta.Situacao.ToString(),
                    DataCriacao = conta.DataCriacao,
                    FormaPagamento = conta.FormaPagamento.ToString(),
                    NumeroMesa = conta.NumeroMesa,
                    TipoDivisao = conta.TipoDivisao.ToString()
                }
            };
        }

        #endregion Conta

        public static SolicitacaoDto MapSolicitacaoCompletaParaDto(Solicitacao solicitacao)
        {
            var conta = solicitacao.Conta;

            return new SolicitacaoDto
            {
                CodigoSolicitacao = solicitacao.Codigo,
                TipoSolicitacao = solicitacao.TipoSolicitacao.ToString(),
                Mensagem = solicitacao.Mensagem,
                Conta = new ContaSimplesDto
                {
                    CodigoConta = conta.Codigo,
                    NumeroMesa = conta.NumeroMesa,
                    DataCriacao = conta.DataCriacao,
                    Situacao = conta.Situacao.ToString(),
                    TipoDivisao = conta.TipoDivisao.ToString(),
                    FormaPagamento = conta.FormaPagamento.ToString()
                }
            };
        }
    }
}