using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CardapioDigital.Dominio.Core;
using CardapioDigital.Dominio.Core.Idioma;

namespace CardapioDigital.Dominio.Estoque
{
    public class Subcategoria : EntidadeBase
    {
        protected Subcategoria()
        {
            this._opcoes = new List<OpcaoSubcategoria>();
            this._produtos = new List<Produto>();
            this._traducoes = new List<SubcategoriaTraducao>();
        }

        public Subcategoria(string nome, string descricao, Categoria categoria, Idioma idioma)
            : this()
        {
            this.Categoria = categoria;
            this.AdicionarTraducao(idioma, nome, descricao);
        }

        public virtual SubcategoriaTraducao TraducaoCorrente
        {
            get { return ObterTraducaoEmIdiomaCorrente(_traducoes); }
        }

        public virtual Categoria Categoria { get; protected set; }

        private readonly ICollection<OpcaoSubcategoria> _opcoes;
        public virtual IEnumerable<OpcaoSubcategoria> Opcoes
        {
            get { return _opcoes; }
        }

        private readonly ICollection<Produto> _produtos;
        public virtual IEnumerable<Produto> Produtos
        {
            get { return _produtos; }
        }

        private readonly ICollection<SubcategoriaTraducao> _traducoes;
        public virtual IEnumerable<SubcategoriaTraducao> Traducoes
        {
            get { return _traducoes; }
        }

        public virtual void AdicionarOpcao(OpcaoSubcategoria opcao)
        {
            this._opcoes.Add(opcao);
        }

        public virtual void RemoverOpcao(OpcaoSubcategoria opcao)
        {
            this._opcoes.Remove(opcao);
        }

        public virtual Produto AdicionarProduto(Produto produto)
        {
            this._produtos.Add(produto);
            
            return produto;
        }

        public virtual Produto AdicionarProduto(string nome, string descricao, decimal preco, string imagem, bool destaque, Idioma idioma)
        {
            var produto = new Produto(nome, descricao, preco, imagem, destaque, idioma, this);

            this._produtos.Add(produto);

            return produto;
        }

        public virtual void RemoverProduto(Produto produto)
        {
            this._produtos.Remove(produto);
        }

        public virtual void AdicionarTraducao(Idioma idioma, string nome, string descricao)
        {
            if (this.Traducoes.Any(t => t.Idioma == idioma))
                throw new IdiomaExistenteException();

            this._traducoes.Add(new SubcategoriaTraducao(idioma, nome, descricao));
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
    }
}
