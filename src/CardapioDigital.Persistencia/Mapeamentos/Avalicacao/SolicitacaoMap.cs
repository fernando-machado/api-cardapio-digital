using CardapioDigital.Dominio.Atendimento;
using CardapioDigital.Persistencia.Mapeamentos.Core;
using FluentNHibernate.Mapping;

namespace CardapioDigital.Persistencia.Mapeamentos.Avalicacao
{
    public class SolicitacaoMap : BaseMap<Solicitacao>
    {
        public SolicitacaoMap()
        {
            Map(x => x.Mensagem);

            Map(x => x.TipoSolicitacao)
                .CustomType<GenericEnumMapper<TipoSolicitacao>>();

            References(x => x.Conta);
        }
    }
}
