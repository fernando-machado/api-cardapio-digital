using CardapioDigital.Dominio.Core;
using FluentNHibernate.Mapping;

namespace CardapioDigital.Persistencia.Mapeamentos.Core
{
    public class BaseMap<T> : ClassMap<T> where T : EntidadeBase
    {
        public BaseMap()
        {
            Id(x => x.Codigo).GeneratedBy.Identity();
        }
    }
}
