using System;
using System.Collections.Generic;
using System.Linq;
using CardapioDigital.Dominio.Core;
using CardapioDigital.Dominio.Core.Idioma;

namespace CardapioDigital.Dominio.Estoque
{
    public class Categoria : EntidadeBase
    {
        protected Categoria()
        {
            this._subcategorias = new List<Subcategoria>();
            this._traducoes = new List<CategoriaTraducao>();
        }

        public Categoria(string nome, string descricao, Idioma idioma)
            : this()
        {
            this.AdicionarTraducao(idioma, nome, descricao);
        }

        public virtual CategoriaTraducao TraducaoCorrente
        {
            get { return ObterTraducaoEmIdiomaCorrente(_traducoes); }
        }

        private readonly ICollection<CategoriaTraducao> _traducoes;
        public virtual IEnumerable<CategoriaTraducao> Traducoes
        {
            get { return _traducoes; }
        }

        private readonly ICollection<Subcategoria> _subcategorias;
        public virtual IEnumerable<Subcategoria> Subcategorias
        {
            get { return _subcategorias; }
        }

        public virtual Subcategoria AdicionarSubcategoria(Subcategoria subcategoria)
        {
            this._subcategorias.Add(subcategoria);
            return subcategoria;
        }

        public virtual void RemoverSubcategoria(Subcategoria subcategoria)
        {
            this._subcategorias.Remove(subcategoria);
        }

        public virtual void AdicionarTraducao(Idioma idioma, string nome, string descricao)
        {
            if (this.Traducoes.Any(t => t.Idioma == idioma))
                throw new IdiomaExistenteException();

            this._traducoes.Add(new CategoriaTraducao(idioma, nome, descricao));
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
