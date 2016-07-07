using CardapioDigital.Dominio.Core.Idioma;

namespace CardapioDigital.Dominio.Estoque
{
    public class OpcaoProduto : Opcao
    {
        protected OpcaoProduto() { }

        public OpcaoProduto(string nome, string descricao, Idioma idioma)
            : base(nome, descricao, idioma)
        {
        }

        public virtual Produto Produto { get; protected set; }
    }
}
