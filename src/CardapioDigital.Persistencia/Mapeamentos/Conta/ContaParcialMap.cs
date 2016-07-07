using CardapioDigital.Dominio.Conta;
using CardapioDigital.Persistencia.Mapeamentos.Core;
using FluentNHibernate.Mapping;

namespace CardapioDigital.Persistencia.Mapeamentos.Conta
{
    public class ContaParcialMap : BaseMap<ContaParcial>
    {
        public ContaParcialMap()
        {
            Map(x => x.Cliente);

            HasManyToMany(x => x.Itens)
                .Access.LowerCaseField(Prefix.Underscore)
                .Fetch.Join()
                .FetchType.Join()
                .Cascade.SaveUpdate()
                .AsSet();
        }
    }
}