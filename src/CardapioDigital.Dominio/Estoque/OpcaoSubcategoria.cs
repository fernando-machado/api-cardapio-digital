using CardapioDigital.Dominio.Core.Idioma;

namespace CardapioDigital.Dominio.Estoque
{
    public class OpcaoSubcategoria : Opcao
    {
        protected OpcaoSubcategoria() { }

        public OpcaoSubcategoria(string nome, string descricao, Idioma idioma)
            : base(nome, descricao, idioma)
        {
        }

        public virtual Subcategoria Subcategoria { get; protected set; }
    }
}