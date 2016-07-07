using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CardapioDigital.Dominio.Core
{
    public interface IRepositorioBase<T> where T : EntidadeBase
    {
        T ObterPorId(int id);

        IList<T> ObterTodos();

        //IQueryable<T> ObterTodos<TProperty>(params Expression<Func<T, TProperty>>[] propriedades);

        IList<T> ObterTodosOnde(Expression<Func<T, bool>> condicao);

        //IList<T> ObterTodosOnde<TProperty>(Expression<Func<T, bool>> condicao, params Expression<Func<T, TProperty>>[] propriedades);

        bool Existe(Expression<Func<T, bool>> condicao);

        int Quantidade(Expression<Func<T, bool>> condicao);

        void Salvar(T entidade);

        void Deletar(T entidade);
    }
}
