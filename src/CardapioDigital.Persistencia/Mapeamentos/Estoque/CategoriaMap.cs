using CardapioDigital.Dominio.Estoque;
using CardapioDigital.Persistencia.Mapeamentos.Core;
using FluentNHibernate.Mapping;

namespace CardapioDigital.Persistencia.Mapeamentos.Estoque
{
    public class CategoriaMap : BaseMap<Categoria>
    {
        public CategoriaMap()
        {
            HasMany(x => x.Traducoes)
                .Access.CamelCaseField(Prefix.Underscore)
                .Fetch.Join()
                .Cascade.AllDeleteOrphan()
                .AsSet();

            HasMany(x => x.Subcategorias)
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.AllDeleteOrphan()
                .AsSet();
        }
    }
}
