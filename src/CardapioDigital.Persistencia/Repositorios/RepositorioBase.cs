using CardapioDigital.Dominio.Core;
using CardapioDigital.Persistencia.InfraNH;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CardapioDigital.Persistencia.Repositorios
{
    public abstract class RepositorioBase<T> : IRepositorioBase<T> where T : EntidadeBase
    {
        private readonly UnidadeDeTrabalho _unidadeDeTrabalho;

        protected RepositorioBase(IUnidadeDeTrabalho unidadeDeTrabalho)
        {
            _unidadeDeTrabalho = (UnidadeDeTrabalho)unidadeDeTrabalho;
        }

        public ISession Sessao
        {
            get { return (_unidadeDeTrabalho).Sessao; }
        }

        public T ObterPorId(int id)
        {
            return Sessao.Get<T>(id);
        }

        public IList<T> ObterTodos()
        {
            return Sessao.Query<T>().ToList();
        }

        public IList<T> ObterTodosOnde(Expression<Func<T, bool>> condicao)
        {
            return Sessao.Query<T>().Where(condicao).ToList();
        }

        public bool Existe(Expression<Func<T, bool>> condicao)
        {
            return Sessao.Query<T>().Any(condicao);
        }

        public int Quantidade(Expression<Func<T, bool>> condicao)
        {
            return Sessao.Query<T>().Count(condicao);
        }

        public void Salvar(T entidade)
        {
            //Sessao.SaveOrUpdate(entidade);
            if (entidade.Codigo == 0)
                Sessao.Save(entidade);
            else
                Sessao.Update(entidade);
        }

        public void Deletar(T entidade)
        {
            Sessao.Delete(entidade);
        }





        //public IQueryable<T> ObterTodos<TProperty>(params Expression<Func<T, TProperty>>[] propriedades)
        //{
        //    var query = _contexto.Set<T>();

        //    return IncluirPropriedades(query, propriedades);
        //}

        //public IList<T> ObterTodosOnde<TProperty>(Expression<Func<T, bool>> condicao, params Expression<Func<T, TProperty>>[] propriedades)
        //{
        //    var query = _contexto.Set<T>();

        //    return IncluirPropriedades(query, propriedades).Where(condicao).ToList();
        //}

        //private static IQueryable<T> IncluirPropriedades<TProperty>(IQueryable<T> query, params Expression<Func<T, TProperty>>[] propriedades)
        //{
        //    return propriedades.Aggregate(query, (currentQuery, propriedade) => currentQuery.Include(propriedade));
        //}
    }
}
