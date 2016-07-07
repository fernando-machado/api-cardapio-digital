using CardapioDigital.Dominio.Core;

namespace CardapioDigital.Dominio.Atendimento
{
    public class Solicitacao : EntidadeBase
    {
        protected Solicitacao() { }

        public Solicitacao(Conta.Conta conta, TipoSolicitacao tipoSolicitacao, string mensagem)
        {
            Conta = conta;
            TipoSolicitacao = tipoSolicitacao;
            Mensagem = mensagem;
        }

        public virtual Conta.Conta Conta { get; protected set; }
        public virtual TipoSolicitacao TipoSolicitacao { get; protected set; }
        public virtual string Mensagem { get; protected set; }
    }
}
