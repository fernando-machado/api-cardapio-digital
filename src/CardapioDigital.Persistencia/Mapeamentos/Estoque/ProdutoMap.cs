using CardapioDigital.Dominio.Estoque;
using CardapioDigital.Persistencia.Mapeamentos.Core;
using FluentNHibernate.Mapping;

namespace CardapioDigital.Persistencia.Mapeamentos.Estoque
{
    public class ProdutoMap : BaseMap<Produto>
    {
        public ProdutoMap()
        {
            Map(x => x.Preco);
            
            Map(x => x.Imagem)
                .Nullable()
                .Length(int.MaxValue);
            
            Map(x => x.Destaque);

            HasMany(x => x.OpcoesProduto)
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.AllDeleteOrphan()
                .AsSet()
                .LazyLoad();

            References(x => x.Subcategoria)
                //.Fetch.Join()
                .Cascade.SaveUpdate()
                .LazyLoad();

            HasMany(x => x.Traducoes)
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.AllDeleteOrphan()
                .AsSet()
                .LazyLoad();
        }
    }
}