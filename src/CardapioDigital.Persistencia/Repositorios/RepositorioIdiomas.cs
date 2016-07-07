using System.Linq;
using CardapioDigital.Dominio.Core;
using CardapioDigital.Dominio.Core.Idioma;

namespace CardapioDigital.Persistencia.Repositorios
{
    public class RepositorioIdiomas : RepositorioBase<Idioma>, IRepositorioIdiomas
    {
        public RepositorioIdiomas(IUnidadeDeTrabalho unidadeDeTrabalho)
            : base(unidadeDeTrabalho)
        {
        }

        public Idioma ObterPorSigla(string sigla)
        {
            return this.ObterTodosOnde(i => i.Sigla == sigla).SingleOrDefault();
        }
    }
}
