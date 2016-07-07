using CardapioDigital.Dominio.Core;
using CardapioDigital.Dominio.Core.Idioma;

namespace CardapioDigital.Dominio.Estoque
{
    public class SubcategoriaTraducao : EntidadeBase, ITraducao
    {
        protected SubcategoriaTraducao() { }

        public SubcategoriaTraducao(Idioma idioma, string nome, string descricao)
        {
            this.Idioma = idioma;
            this.Nome = nome;
            this.Descricao = descricao;
        }

        public virtual Idioma Idioma { get; protected internal set; }
        public virtual string Nome { get; protected internal set; }
        public virtual string Descricao { get; protected internal set; }
    }
}
