using System;
using System.Collections.Generic;
using System.Linq;
using CardapioDigital.Dominio.Core;
using CardapioDigital.Dominio.Core.Idioma;

namespace CardapioDigital.Dominio.Estoque
{
    public class Produto : EntidadeBase
    {
        protected Produto()
        {
            this._opcoesProduto = new List<OpcaoProduto>();
            this._traducoes = new List<ProdutoTraducao>();
            this.Destaque = false;
        }

        public Produto(string nome, string descricao, decimal preco, string imagem, Idioma idioma, Subcategoria subcategoria)
            : this()
        {
            this.AdicionarTraducao(idioma, nome, descricao);
            this.Preco = preco;
            this.Imagem = imagem;
            this.Subcategoria = subcategoria;
        }

        public Produto(string nome, string descricao, decimal preco, string imagem, bool destaque, Idioma idioma, Subcategoria subcategoria)
            : this(nome, descricao, preco, imagem, idioma, subcategoria)
        {
            this.Destaque = destaque;
        }

        public virtual decimal Preco { get; protected set; }
        public virtual string Imagem { get; protected set; }
        public virtual Subcategoria Subcategoria { get; protected set; }
        public virtual bool Destaque { get; set; }

        public virtual ProdutoTraducao TraducaoCorrente
        {
            get { return ObterTraducaoEmIdiomaCorrente(_traducoes); }
        }

        public virtual IEnumerable<Opcao> Opcoes
        {
            get
            {
                var opcoesSubcategoria = Subcategoria.Opcoes.ToList<Opcao>();
                return _opcoesProduto.Union(opcoesSubcategoria);
            }
        }

        private readonly ICollection<OpcaoProduto> _opcoesProduto;
        public virtual IEnumerable<OpcaoProduto> OpcoesProduto
        {
            get { return _opcoesProduto; }
        }

        private readonly ICollection<ProdutoTraducao> _traducoes;
        public virtual IEnumerable<ProdutoTraducao> Traducoes
        {
            get { return _traducoes; }
        }

        public virtual void AdicionarOpcao(OpcaoProduto opcao)
        {
            this._opcoesProduto.Add(opcao);
        }

        public virtual void RemoverOpcao(OpcaoProduto opcao)
        {
            this._opcoesProduto.Remove(opcao);
        }

        public virtual void AdicionarTraducao(Idioma idioma, string nome, string descricao)
        {
            if (this.Traducoes.Any(t => t.Idioma == idioma))
                throw new IdiomaExistenteException();

            this._traducoes.Add(new ProdutoTraducao(idioma, nome, descricao));
        }

        public virtual void AlterarSubcategoria(Subcategoria novaSubcategoria)
        {
            if (novaSubcategoria == null)
                throw new ArgumentNullException("novaSubcategoria");

            this.Subcategoria = novaSubcategoria;
        }

        public virtual void AlterarTraducao(Idioma idioma, string novoNome, string novaDescricao)
        {
            if (idioma == null)
                throw new ArgumentNullException("idioma");

            if (novoNome == null)
                throw new ArgumentNullException("novoNome");

            if (novaDescricao == null)
                throw new ArgumentNullException("novaDescricao");

            var traducao = Traducoes.SingleOrDefault(t => t.Idioma == idioma);
            if (traducao == null)
                throw new IdiomaExistenteException();

            traducao.Nome = novoNome;
            traducao.Descricao = novaDescricao;
        }

        public virtual void AlterarPreco(decimal novoPreco)
        {
            if (novoPreco >= 0)
                this.Preco = novoPreco;
        }

        public virtual void AlterarImagem(string novaImagem)
        {
            this.Imagem = novaImagem;
        }
    }
}
