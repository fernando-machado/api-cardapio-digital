using CardapioDigital.Dominio.Core;
using CardapioDigital.Dominio.Estoque;

namespace CardapioDigital.Persistencia.Repositorios
{
    public class Categorias : RepositorioBase<Categoria>, ICategorias
    {
        public Categorias(IUnidadeDeTrabalho unidadeDeTrabalho)
            : base(unidadeDeTrabalho)
        {

        }
    }
}