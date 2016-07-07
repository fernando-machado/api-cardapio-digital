using CardapioDigital.Dominio.Core;
using CardapioDigital.Dominio.Estoque;

namespace CardapioDigital.Persistencia.Repositorios
{
    public class RepositorioDeOpcoes : RepositorioBase<Opcao>, IRepositorioDeOpcoes
    {
        public RepositorioDeOpcoes(IUnidadeDeTrabalho unidadeDeTrabalho)
            : base(unidadeDeTrabalho)
        {
        }
    }
}
