namespace CardapioDigital.Dominio.Core.Idioma
{
    public interface IRepositorioIdiomas : IRepositorioBase<Idioma>
    {
        Idioma ObterPorSigla(string sigla);
    }
}
