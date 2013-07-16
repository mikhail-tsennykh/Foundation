using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Data.Data.Infrastructure {

  public abstract class RepositoryBase<T> where T : class {

    private DataContext _dataContext;
    private readonly IDbSet<T> _dbset;

    protected IDatabaseFactory DatabaseFactory { get; private set; }

    protected DataContext DataContext {
      get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); }
    }

    protected RepositoryBase(IDatabaseFactory databaseFactory) {
      DatabaseFactory = databaseFactory;
      _dbset = DataContext.Set<T>();
    }

    public virtual IDbSet<T> GetAll() {
      return _dbset;
    }
    public virtual IEnumerable<T> GetMany(Func<T, bool> where) {
      return _dbset.Where(where);
    }

    public virtual T Get(int id) {
      return _dbset.Find(id);
    }

    public T Get(Func<T, Boolean> where) {
      return _dbset.Where(where).FirstOrDefault();
    }

    public virtual void Add(T entity) {
      _dbset.Add(entity);
    }

    public virtual T Delete(int id) {
      var entity = _dbset.Find(id);
      if (entity != null)
        _dbset.Remove(entity);
      return entity;
    }

    public virtual void Delete(T entity) {
      _dbset.Remove(entity);
    }

    // do not use another list of data to compare, only simple values
    public void Delete(Func<T, Boolean> where) {
      var objects = _dbset.Where(where).AsEnumerable();
      foreach (T obj in objects)
        _dbset.Remove(obj);
    }

  }
}