using CardapioDigital.Dominio.Atendimento;
using CardapioDigital.Dominio.Core;

namespace CardapioDigital.Persistencia.Repositorios
{
    public class RepositorioAvaliacoes : RepositorioBase<Avaliacao>, IRepositorioAvaliacoes
    {
        public RepositorioAvaliacoes(IUnidadeDeTrabalho unidadeDeTrabalho) 
            : base(unidadeDeTrabalho)
        {
            
        }
    }
}
