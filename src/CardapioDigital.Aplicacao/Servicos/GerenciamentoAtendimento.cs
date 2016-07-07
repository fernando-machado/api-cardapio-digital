using System;
using System.Collections.Generic;
using System.Linq;
using CardapioDigital.Aplicacao.DTO;
using CardapioDigital.Dominio.Atendimento;
using CardapioDigital.Dominio.Atendimento.Exceptions;
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

        public AvaliacaoCompletaDto ObterAvaliacao(int codigoAvaliacao)
        {
            var avaliacao = _repositorioAvaliacoes.ObterPorId(codigoAvaliacao);
            if (avaliacao == null)
                throw new AvaliacaoNaoEncontradaException("Não foi possível encontrar a avaliação com código {0}", codigoAvaliacao);

            return MapeamentoDtoHelper.MapAvaliacaoCompletaParaDto(avaliacao);
        }

        public IEnumerable<SolicitacaoDto> ObterTodasSolicitacoes()
        {
            var solicitacoes = _repositorioSolicitacoes.ObterTodos();

            return solicitacoes.Select(MapeamentoDtoHelper.MapSolicitacaoCompletaParaDto);
        }

        public IEnumerable<SolicitacaoDto> ObterSolicitacoesDaConta(int codigoConta)
        {
            var solicitacoesDaConta = _repositorioSolicitacoes.ObterTodosOnde(s => s.Conta.Codigo == codigoConta);

            return solicitacoesDaConta.Select(MapeamentoDtoHelper.MapSolicitacaoCompletaParaDto);
        }

        public SolicitacaoDto ObterSolicitacao(int codigoSolicitacao)
        {
            var solicitacao = _repositorioSolicitacoes.ObterPorId(codigoSolicitacao);
            if (solicitacao == null)
                throw new SolicitacaoNaoEncontradaException("Não foi possível encontrar a solicitacao com código {0}", codigoSolicitacao);

            return MapeamentoDtoHelper.MapSolicitacaoCompletaParaDto(solicitacao);
        }
    }
}