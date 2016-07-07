using CardapioDigital.Dominio.Estoque;
using CardapioDigital.Persistencia.Mapeamentos.Core;
using FluentNHibernate.Mapping;

namespace CardapioDigital.Persistencia.Mapeamentos.Estoque
{
    public class SubcategoriaMap : BaseMap<Subcategoria>
    {
        public SubcategoriaMap()
        {
            HasOne(x => x.Categoria)
                .Cascade.All();

            HasMany(x => x.Traducoes)
                .Access.CamelCaseField(Prefix.Underscore)
                .Fetch.Join()
                .Cascade.AllDeleteOrphan()
                .AsSet();

            HasMany(x => x.Opcoes)
                .Access.CamelCaseField(Prefix.Underscore)
                .Fetch.Join()
                .Cascade.AllDeleteOrphan()
                .AsSet();

            HasMany(x => x.Produtos)
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.AllDeleteOrphan()
                .AsSet();
        }
    }
}