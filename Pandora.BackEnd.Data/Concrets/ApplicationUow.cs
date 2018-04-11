using Pandora.BackEnd.Data.Contracts;
using Pandora.BackEnd.Model.Users;
using System;

namespace Pandora.BackEnd.Data.Concrets
{
    public class ApplicationUow : IApplicationUow, IDisposable
    {
        private ApplicationDbContext _dbContext;
        private readonly IRepositoryProvider _repositoryProvider;


        public ApplicationUow(IRepositoryProvider repositoryProvider)
        {
            CreateDbContext();

            repositoryProvider.DbContext = _dbContext;
            _repositoryProvider = repositoryProvider;
        }

        // Repositories
        public IRepository<Employee> EmployeeRepository => GetStandardRepo<Employee>();

        /// <summary>
        /// Save pending changes to the database
        /// </summary>
        public bool Commit()
        {
            //System.Diagnostics.Debug.WriteLine("Committed");
            return _dbContext.SaveChanges() > 0;
        }

        private void CreateDbContext()
        {
            _dbContext = new ApplicationDbContext();

            // Do NOT enable proxied entities, else serialization fails
            _dbContext.Configuration.ProxyCreationEnabled = false;
            
            // Load navigation properties explicitly (avoid serialization trouble)
            _dbContext.Configuration.LazyLoadingEnabled = false;

            // Because Web API will perform validation, we don't need/want EF to do so
            _dbContext.Configuration.ValidateOnSaveEnabled = false;

            //DbContext.Configuration.AutoDetectChangesEnabled = false;
            // We won't use this performance tweak because we don't need 
            // the extra performance and, when autodetect is false,
            // we'd have to be careful. We're not being that careful.
        }


        private IRepository<T> GetStandardRepo<T>() where T : class
        {
            return _repositoryProvider.GetRepositoryForEntityType<T>();
        }

        private T GetRepo<T>() where T : class
        {
            return _repositoryProvider.GetRepository<T>();
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext?.Dispose();
            }
        }

        #endregion
    }
}
