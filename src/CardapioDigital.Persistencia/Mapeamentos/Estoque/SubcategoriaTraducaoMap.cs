using CardapioDigital.Dominio.Estoque;
using CardapioDigital.Persistencia.Mapeamentos.Core;

namespace CardapioDigital.Persistencia.Mapeamentos.Estoque
{
    public class SubcategoriaTraducaoMap : BaseMap<SubcategoriaTraducao>
    {
        public SubcategoriaTraducaoMap()
        {
            Map(x => x.Nome);

            Map(x => x.Descricao);

            References(x => x.Idioma);
        }
    }
}
