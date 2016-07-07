using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CardapioDigital.Dominio.Core.Idioma;

namespace CardapioDigital.Dominio.Core
{
    public abstract class EntidadeBase
    {
        public virtual int Codigo { get; protected set; }

        public static bool operator ==(EntidadeBase entidade1, EntidadeBase entidade2)
        {
            if (Equals(entidade1, null) && Equals(entidade2, null)) return true;

            if (Equals(entidade1, null) || Equals(entidade2, null)) return false;

            if (entidade1.Codigo == 0 || entidade2.Codigo == 0) return false;

            return entidade1.Codigo == entidade2.Codigo;
        }

        public static bool operator !=(EntidadeBase entidade1, EntidadeBase entidade2)
        {
            return !(entidade1 == entidade2);
        }

        public override bool Equals(object entidade)
        {
            return (this == entidade as EntidadeBase);
        }

        public override int GetHashCode()
        {
            return Codigo.GetHashCode();
        }

        public virtual T ObterTraducaoEmIdiomaCorrente<T>(IEnumerable<T> traducoes) where T : ITraducao
        {
            var cultureNameCurrentThread = Thread.CurrentThread.CurrentCulture.TextInfo.CultureName;

            var idiomaCorrente = traducoes.SingleOrDefault(t => t.Idioma.Sigla == cultureNameCurrentThread);
            if (idiomaCorrente == null)
                idiomaCorrente = traducoes.Single(t => t.Idioma.Sigla == "pt-BR");

            return idiomaCorrente;
        }
    }
}
