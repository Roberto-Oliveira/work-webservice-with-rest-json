using RepositoryBLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace RepositoryDAO
{
    public abstract class GenericDAO<T, DC> where T : class where DC : DataContext, new()
    {
        private readonly DC _context = new DC();

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void Insert(T entity)
        {
            _context.GetTable<T>().InsertOnSubmit(entity);
            _context.SubmitChanges();
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Update(T entity)
        {
            _context.Refresh(RefreshMode.KeepCurrentValues, entity);
            _context.SubmitChanges();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Delete(T entity)
        {
            _context.GetTable<T>().DeleteOnSubmit(entity);
            _context.SubmitChanges();
        }

        public virtual ITable GetClass()
        {
            return _context.GetTable<T>();
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public IQueryable<T> GetTable()
        {
            return _context.GetTable<T>();
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public IList<T> GetList()
        {
            return _context.GetTable<T>().ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public T SearchById(int? Id)
        {
            var parametro = Expression.Parameter(typeof(T), "item");

            var expressao = Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.Property(parametro, typeof(T)
                                      .GetPrimaryKey().Name),
                            Expression.Constant(Id)), parametro);

            var item = GetTable().Where(expressao).SingleOrDefault();

            return item;
        }
    }
}
