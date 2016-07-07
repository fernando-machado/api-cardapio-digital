using CardapioDigital.Dominio.Estoque;
using FluentNHibernate.Mapping;

namespace CardapioDigital.Persistencia.Mapeamentos.Estoque
{
    public class OpcaoSubcategoriaMap : SubclassMap<OpcaoSubcategoria>
    {
        public OpcaoSubcategoriaMap()
        {
            KeyColumn("CodigoOpcao");

            References(x => x.Subcategoria);
        }
    }
}