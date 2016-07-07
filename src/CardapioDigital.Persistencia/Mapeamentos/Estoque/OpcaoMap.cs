using CardapioDigital.Dominio.Estoque;
using CardapioDigital.Persistencia.Mapeamentos.Core;
using FluentNHibernate.Mapping;

namespace CardapioDigital.Persistencia.Mapeamentos.Estoque
{
    public class OpcaoMap : BaseMap<Opcao>
    {
        public OpcaoMap()
        {
            HasMany(x => x.Traducoes)
                .Access.CamelCaseField(Prefix.Underscore)
                .Fetch.Join()
                .Cascade.AllDeleteOrphan()
                .AsSet();
        }
    }
}