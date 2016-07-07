using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardapioDigital.Dominio.Core;
using CardapioDigital.Dominio.Core.Idioma;

namespace CardapioDigital.Dominio.Estoque
{
    public abstract class Opcao : EntidadeBase
    {
        protected Opcao()
        {
            _traducoes = new List<OpcaoTraducao>();
        }

        protected Opcao(string nome, string descricao, Idioma idioma)
            : this()
        {
            this.AdicionarTraducao(idioma, nome, descricao);
        }

        public virtual OpcaoTraducao TraducaoCorrente
        {
            get { return ObterTraducaoEmIdiomaCorrente(_traducoes); }
        }

        private readonly ICollection<OpcaoTraducao> _traducoes;
        public virtual IEnumerable<OpcaoTraducao> Traducoes
        {
            get { return _traducoes; }
        }

        public virtual void AdicionarTraducao(Idioma idioma, string nome, string descricao)
        {
            if (this.Traducoes.Any(t => t.Idioma == idioma))
                throw new IdiomaExistenteException();

            this._traducoes.Add(new OpcaoTraducao(idioma, nome, descricao));
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
