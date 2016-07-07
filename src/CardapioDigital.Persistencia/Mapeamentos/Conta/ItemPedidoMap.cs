using CardapioDigital.Dominio.Conta;
using FluentNHibernate.Mapping;

namespace CardapioDigital.Persistencia.Mapeamentos.Conta
{
    public class ItemPedidoMap : ClassMap<ItemPedido>
    {
        public ItemPedidoMap()
        {
            Id(x => x.Codigo).GeneratedBy.Identity();

            References(x => x.Produto);

            Map(x => x.SituacaoPreparo)
                .CustomType<GenericEnumMapper<SituacaoPreparo>>();

            HasManyToMany(x => x.OpcoesSelecionadas)
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.AllDeleteOrphan()
                .AsSet()
                .Table("ItemPedidoOpcoesSelecionadas");
        }
    }
}
