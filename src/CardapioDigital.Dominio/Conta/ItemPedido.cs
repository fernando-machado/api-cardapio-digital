using System;
using System.Collections.Generic;
using CardapioDigital.Dominio.Conta.Exceptions;
using CardapioDigital.Dominio.Core;
using CardapioDigital.Dominio.Estoque;

namespace CardapioDigital.Dominio.Conta
{
    public class ItemPedido : EntidadeBase
    {
        protected ItemPedido()
        {
            this._opcoesSelecionadas = new List<Opcao>();
            this.SituacaoPreparo = SituacaoPreparo.AguardandoPreparacao;
        }

        public ItemPedido(Produto produto)
            : this()
        {
            if (produto == null)
                throw new ArgumentNullException("produto");

            this.Produto = produto;
        }

        public virtual Produto Produto { get; protected set; }
        public virtual SituacaoPreparo SituacaoPreparo { get; protected set; }

        private readonly ICollection<Opcao> _opcoesSelecionadas;
        public virtual IEnumerable<Opcao> OpcoesSelecionadas
        {
            get { return _opcoesSelecionadas; }
        }

        public virtual void AdicionarOpcao(Opcao opcao)
        {
            this._opcoesSelecionadas.Add(opcao);
        }

        public virtual void AtualizarSituacaoPreparo(SituacaoPreparo novaSituacaoPreparo)
        {
            if ((int)novaSituacaoPreparo <= (int)this.SituacaoPreparo)
                throw new SituacaoPreparoException("Não é possível voltar a situação de preparo do item de {0} para {1}", this.SituacaoPreparo.ToString(), novaSituacaoPreparo.ToString());

            this.SituacaoPreparo = novaSituacaoPreparo;
        }
    }
}