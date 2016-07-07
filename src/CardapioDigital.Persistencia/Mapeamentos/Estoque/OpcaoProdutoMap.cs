using CardapioDigital.Dominio.Estoque;
using FluentNHibernate.Mapping;

namespace CardapioDigital.Persistencia.Mapeamentos.Estoque
{
    public class OpcaoProdutoMap : SubclassMap<OpcaoProduto>
    {
        public OpcaoProdutoMap()
        {
            KeyColumn("CodigoOpcao");

            References(x => x.Produto);
        }
    }
}