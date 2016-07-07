namespace CardapioDigital.Dominio.Core
{
    public interface IUnidadeDeTrabalho : System.IDisposable
    {
        void Commit();

        void Rollback();
    }
}
