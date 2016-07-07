using CardapioDigital.Dominio.Estoque;
using CardapioDigital.Persistencia.Mapeamentos.Core;

namespace CardapioDigital.Persistencia.Mapeamentos.Estoque
{
    public class CategoriaTraducaoMap : BaseMap<CategoriaTraducao>
    {
        public CategoriaTraducaoMap()
        {
            Map(x => x.Nome);

            Map(x => x.Descricao);

            References(x => x.Idioma);
        }
    }
}
