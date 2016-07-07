using CardapioDigital.Dominio.Core.Idioma;

namespace CardapioDigital.Persistencia.Mapeamentos.Core
{
    public class IdiomaMap : BaseMap<Idioma>
    {
        public IdiomaMap()
        {
            Map(x => x.Nome);

            Map(x => x.Sigla);
        }
    }
}
