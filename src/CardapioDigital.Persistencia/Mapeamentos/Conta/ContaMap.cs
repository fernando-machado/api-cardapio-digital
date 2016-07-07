using CardapioDigital.Dominio.Conta;
using CardapioDigital.Persistencia.Mapeamentos.Core;
using FluentNHibernate.Mapping;

namespace CardapioDigital.Persistencia.Mapeamentos.Conta
{
    public class ContaMap : BaseMap<Dominio.Conta.Conta>
    {
        public ContaMap()
        {
            Map(x => x.NumeroMesa);

            Map(x => x.DataCriacao);

            Map(x => x.Situacao)
                .CustomType<GenericEnumMapper<SituacaoConta>>();

            Map(x => x.FormaPagamento)
                .CustomType<GenericEnumMapper<FormaPagamento>>()
                .Nullable();

            Map(x => x.TipoDivisao)
                .CustomType<GenericEnumMapper<TipoDivisaoConta>>()
                .Nullable();

            References(x => x.Avaliacao)
                .Cascade.All()
                .Nullable();

            HasMany(x => x.Parciais)
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.AllDeleteOrphan()
                .AsSet();

            HasMany(x => x.Solicitacoes)
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.AllDeleteOrphan()
                .AsSet();

            HasManyToMany(x => x.Pedido)
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.SaveUpdate()
                .AsSet();
        }
    }
}
