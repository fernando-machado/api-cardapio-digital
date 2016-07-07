using CardapioDigital.Dominio.Core;

namespace CardapioDigital.Dominio.Atendimento
{
    public class Avaliacao : EntidadeBase
    {
        protected Avaliacao() { }

        public Avaliacao(Conta.Conta conta, byte notaGarcom, byte notaAtendimento, byte notaAmbiente, byte notaTempoAtendimento, byte notaCardapioTablet)
            : this()
        {
            this.Conta = conta;
            this.NotaGarcom = notaGarcom;
            this.NotaAtendimento = notaAtendimento;
            this.NotaAmbiente = notaAmbiente;
            this.NotaTempoAtendimento = notaTempoAtendimento;
            this.NotaCardapioTablet = notaCardapioTablet;
        }

        public virtual Conta.Conta Conta { get; protected set; }
        public virtual byte NotaGarcom { get; protected set; }
        public virtual byte NotaAtendimento { get; protected set; }
        public virtual byte NotaAmbiente { get; protected set; }
        public virtual byte NotaTempoAtendimento { get; protected set; }
        public virtual byte NotaCardapioTablet { get; protected set; }
    }
}
