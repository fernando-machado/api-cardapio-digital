using CardapioDigital.Dominio.Core;
using CardapioDigital.Dominio.Estoque;

namespace CardapioDigital.Persistencia.Repositorios
{
    public class Produtos : RepositorioBase<Produto>, IProdutos
    {
        public Produtos(IUnidadeDeTrabalho unidadeDeTrabalho) 
            : base(unidadeDeTrabalho)
        {
            
        }
    }
}
