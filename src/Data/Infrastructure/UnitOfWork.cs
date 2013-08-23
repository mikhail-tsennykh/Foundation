using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace Data.Infrastructure {
	public interface IUnitOfWork {
		void Commit();
		string GetDatabaseSchema();
	}
	public class UnitOfWork : IUnitOfWork {
		private readonly IDatabaseFactory _databaseFactory;
		private DataContext _dataContext;
		public UnitOfWork(IDatabaseFactory databaseFactory) {
			_databaseFactory = databaseFactory;
		}

		protected DataContext DataContext {
			get { return _dataContext ?? (_dataContext = _databaseFactory.Get()); }
		}
		public IEnumerable<DbEntityValidationResult> GetValidationErrors() {
			return DataContext.GetValidationErrors();
		}
		public string GetDatabaseSchema() {
			return DataContext.DumpScript();
		}
		public void Commit() {
			DataContext.Commit();
		}
	}
}