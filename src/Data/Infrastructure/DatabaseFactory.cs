using System;

namespace Data.Data.Infrastructure {
	public interface IDatabaseFactory : IDisposable {
		DataContext Get();
	}
	public class DatabaseFactory : Disposable, IDatabaseFactory {
		private DataContext _dataContext;
		public DataContext Get() {
			if (_dataContext != null) return _dataContext;
			return _dataContext = new DataContext();
		}
		protected override void DisposeCore() {
			if (_dataContext != null)
				_dataContext.Dispose();
		}
	}
	public class Disposable : IDisposable {
		private bool _isDisposed;
		~Disposable() {
			Dispose(false);
		}

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		private void Dispose(bool disposing) {
			if (!_isDisposed && disposing) DisposeCore();
			_isDisposed = true;
		}
		protected virtual void DisposeCore() {}
	}
}