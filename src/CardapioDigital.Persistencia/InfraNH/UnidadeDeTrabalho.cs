using CardapioDigital.Dominio.Core;
using NHibernate;

namespace CardapioDigital.Persistencia.InfraNH
{
    public class UnidadeDeTrabalho : IUnidadeDeTrabalho
    {
        internal ISession Sessao { get; private set; }

        public UnidadeDeTrabalho()
        {
            Sessao = SessionFactory.Instancia.ObterSessao();

            Sessao.FlushMode = FlushMode.Commit;
            Sessao.BeginTransaction();
        }

        public void Commit()
        {
            Sessao.Flush();
            Sessao.Transaction.Commit();
        }

        public void Rollback()
        {
            Sessao.Transaction.Rollback();
        }

        public void Dispose()
        {
            Sessao.Close();
            Sessao.Dispose();
        }
    }
}