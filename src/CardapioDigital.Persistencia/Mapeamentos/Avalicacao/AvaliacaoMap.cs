using CardapioDigital.Dominio.Atendimento;
using CardapioDigital.Persistencia.Mapeamentos.Core;

namespace CardapioDigital.Persistencia.Mapeamentos.Avalicacao
{
    public class AvaliacaoMap : BaseMap<Avaliacao>
    {
        public AvaliacaoMap()
        {
            Map(x => x.NotaGarcom);
            Map(x => x.NotaAtendimento);
            Map(x => x.NotaAmbiente);
            Map(x => x.NotaTempoAtendimento);
            Map(x => x.NotaCardapioTablet);

            References(x => x.Conta);
        }
    }
}
