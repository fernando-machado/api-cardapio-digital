using CardapioDigital.Dominio.Atendimento;

namespace CardapioDigital.Aplicacao.DTO
{
    public class NovaAvaliacaoDto
    {
        public byte NotaGarcom { get; set; }
        public byte NotaAtendimento { get; set; }
        public byte NotaAmbiente { get; set; }
        public byte NotaTempoAtendimento { get; set; }
        public byte NotaCardapioTablet { get; set; }
    }

    public class AvaliacaoSimplesDto
    {
        public int CodigoAvaliacao { get; set; }
        public byte NotaGarcom { get; set; }
        public byte NotaAtendimento { get; set; }
        public byte NotaAmbiente { get; set; }
        public byte NotaTempoAtendimento { get; set; }
        public byte NotaCardapioTablet { get; set; }
    }

    public class AvaliacaoCompletaDto
    {
        public int CodigoAvaliacao { get; set; }
        public byte NotaGarcom { get; set; }
        public byte NotaAtendimento { get; set; }
        public byte NotaAmbiente { get; set; }
        public byte NotaTempoAtendimento { get; set; }
        public byte NotaCardapioTablet { get; set; }
        public ContaSimplesDto Conta { get; set; }
    }

    public class NovaSolicitacaoDto
    {
        public int TipoSolicitacao { get; set; }
        public string Mensagem { get; set; }
    }

    public class SolicitacaoDto
    {
        public int CodigoSolicitacao { get; set; }
        public string TipoSolicitacao { get; set; }
        public string Mensagem { get; set; }
        public ContaSimplesDto Conta { get; set; }
    }
}
