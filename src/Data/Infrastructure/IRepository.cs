using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Data.Infrastructure {
  public interface IRepository<T> where T : class {

    /// <summary>
    /// Gets all records from the database
    /// </summary>
    /// <returns>Records</returns>
    IDbSet<T> GetAll();

    /// <summary>
    /// Gets many records from the database based on selector
    /// </summary>
    /// <param name="where">Selector expression</param>
    /// <returns>Records</returns>
    IEnumerable<T> GetMany(Func<T, bool> where);

    /// <summary>
    /// Gets single record from the database by record's Id
    /// </summary>
    /// <param name="id">Id of the record</param>
    /// <returns>Record</returns>
    T Get(int id);

    /// <summary>
    /// Gets single record from the database based on selector
    /// </summary>
    /// <param name="where">Selector expression</param>
    /// <returns>Record</returns>
    T Get(Func<T, Boolean> where);

    /// <summary>
    /// Adds record to the database
    /// </summary>
    /// <param name="entity">Record to add</param>
    void Add(T entity);

    /// <summary>
    /// Deletes single record
    /// </summary>
    /// <param name="id">Id of the record to delete</param>
    /// <returns>Deleted record</returns>
    T Delete(int id);

    /// <summary>
    /// Deletes single record
    /// </summary>
    /// <param name="entity">Record to delete</param>
    void Delete(T entity);

    /// <summary>
    /// Deletes all records based on selector.
    /// ATTENTION! Do not use another list of data to compare,
    /// use simple values only!
    /// </summary>
    /// <param name="predicate">Selector expression</param>
    void Delete(Func<T, Boolean> predicate);

  }
}