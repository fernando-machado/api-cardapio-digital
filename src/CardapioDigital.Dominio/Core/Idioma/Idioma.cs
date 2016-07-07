namespace CardapioDigital.Dominio.Core.Idioma
{
    public class Idioma : EntidadeBase
    {
        protected Idioma() { }

        public Idioma(string nome, string sigla)
        {
            this.Nome = nome;
            this.Sigla = sigla;
        }

        public virtual string Nome { get; protected set; }
        public virtual string Sigla { get; protected set; }
    }
}
