using System;
using System.Collections.Generic;
using System.Linq;
using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Dominio.Atendimento;
using CardapioDigital.Dominio.Atendimento.Exceptions;
using CardapioDigital.Dominio.Conta;
using CardapioDigital.Dominio.Core;

namespace CardapioDigital.Aplicacao.Servicos
{
    public class GerenciamentoAtendimento
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IRepositorioAvaliacoes _repositorioAvaliacoes;
        private readonly IRepositorioSolicitacoes _repositorioSolicitacoes;

        public GerenciamentoAtendimento(IUnidadeDeTrabalho unidadeDeTrabalho, IRepositorioAvaliacoes repositorioAvaliacoes, IRepositorioSolicitacoes repositorioSolicitacoes)
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _repositorioAvaliacoes = repositorioAvaliacoes;
            _repositorioSolicitacoes = repositorioSolicitacoes;
        }

        public IEnumerable<AvaliacaoCompletaDto> ObterTodasAvaliacoes()
        {
            var avaliacoes = _repositorioAvaliacoes.ObterTodos();

            return avaliacoes.ToList().Select(MapeamentoDtoHelper.MapAvaliacaoCompletaParaDto);
        }

        public AvaliacaoCompletaDto ObterAvaliacaoPorId(int codigoAvaliacao)
        {
            var avaliacao = _repositorioAvaliacoes.ObterPorId(codigoAvaliacao);
            if (avaliacao == null)
                throw new AvaliacaoNaoEncontradaException("Não foi possível encontrar a avaliação com código {0}", codigoAvaliacao);

            return MapeamentoDtoHelper.MapAvaliacaoCompletaParaDto(avaliacao);
        }

        public IEnumerable<AvaliacaoCompletaDto> ObterAvaliacoesPorData(DateTime data)
        {
            var avaliacoes = _repositorioAvaliacoes.ObterTodosOnde(av => av.Conta.DataCriacao.Date == data.Date);

            return avaliacoes.Select(MapeamentoDtoHelper.MapAvaliacaoCompletaParaDto);
        }



        public IEnumerable<SolicitacaoDto> ObterTodasSolicitacoes()
        {
            var solicitacoes = _repositorioSolicitacoes.ObterTodos();

            return solicitacoes.Select(MapeamentoDtoHelper.MapSolicitacaoCompletaParaDto);
        }

        public IEnumerable<SolicitacaoDto> ObterTodasSolicitacoesPendentes()
        {
            //TODO: Implementar Status das solicitações (Pendente | Atendida | Cancelada)
            var solicitacoes = _repositorioSolicitacoes.ObterTodos();

            return solicitacoes.Select(MapeamentoDtoHelper.MapSolicitacaoCompletaParaDto);
        }

        public IEnumerable<SolicitacaoDto> ObterSolicitacoesDaConta(int codigoConta)
        {
            var solicitacoesDaConta = _repositorioSolicitacoes.ObterTodosOnde(s => s.Conta.Codigo == codigoConta);

            return solicitacoesDaConta.Select(MapeamentoDtoHelper.MapSolicitacaoCompletaParaDto);
        }

        public IEnumerable<SolicitacaoDto> ObterSolicitacoesPendentesDaConta(int codigoConta)
        {
            //TODO: Implementar Status das solicitações (Pendente | Atendida | Cancelada)
            var solicitacoesDaConta = _repositorioSolicitacoes.ObterTodosOnde(s => s.Conta.Codigo == codigoConta && s.Conta.Situacao == SituacaoConta.Aberta);

            return solicitacoesDaConta.Select(MapeamentoDtoHelper.MapSolicitacaoCompletaParaDto);
        }

        public SolicitacaoDto ObterSolicitacaoPorId(int codigoSolicitacao)
        {
            var solicitacao = _repositorioSolicitacoes.ObterPorId(codigoSolicitacao);
            if (solicitacao == null)
                throw new SolicitacaoNaoEncontradaException("Não foi possível encontrar a solicitacao com código {0}", codigoSolicitacao);

            return MapeamentoDtoHelper.MapSolicitacaoCompletaParaDto(solicitacao);
        }
    }
}