using CardapioDigital.Dominio.Conta;
using CardapioDigital.Dominio.Core;

namespace CardapioDigital.Persistencia.Repositorios
{
    public class RepositorioDeContas: RepositorioBase<Conta>, IRepositorioDeContas
    {
        public RepositorioDeContas(IUnidadeDeTrabalho unidadeDeTrabalho) 
            : base(unidadeDeTrabalho)
        {
        }
    }
}
