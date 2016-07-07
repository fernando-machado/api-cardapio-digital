using CardapioDigital.Dominio.Estoque;
using CardapioDigital.Persistencia.Mapeamentos.Core;

namespace CardapioDigital.Persistencia.Mapeamentos.Estoque
{
    public class ProdutoTraducaoMap : BaseMap<ProdutoTraducao>
    {
        public ProdutoTraducaoMap()
        {
            Map(x => x.Nome);

            Map(x => x.Descricao);

            References(x => x.Idioma);
        }
    }
}
