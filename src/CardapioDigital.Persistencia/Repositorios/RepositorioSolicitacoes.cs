using CardapioDigital.Dominio.Atendimento;
using CardapioDigital.Dominio.Core;

namespace CardapioDigital.Persistencia.Repositorios
{
    public class RepositorioSolicitacoes : RepositorioBase<Solicitacao>, IRepositorioSolicitacoes
    {
        public RepositorioSolicitacoes(IUnidadeDeTrabalho unidadeDeTrabalho) 
            : base(unidadeDeTrabalho)
        {
            
        }
    }
}
