using Pandora.BackEnd.Data.Concrets;
using Pandora.BackEnd.Data.Contracts;
using System;
using System.Collections.Generic;

namespace Pandora.BackEnd.Data.Helpers
{
    /// <summary>
    /// A maker of Code Camper Repositories.
    /// </summary>
    /// <remarks>
    /// An instance of this class contains repository factory functions for different types.
    /// Each factory function takes an EF <see cref="DbContext"/> and returns
    /// a repository bound to that DbContext.
    /// <para>
    /// Designed to be a "Singleton", configured at web application start with
    /// all of the factory functions needed to create any type of repository.
    /// Should be thread-safe to use because it is configured at app start,
    /// before any request for a factory, and should be immutable thereafter.
    /// </para>
    /// </remarks>
    public class RepositoryFactories
    {
        /// <summary>
        /// Get the dictionary of repository factory functions.
        /// </summary>
        /// <remarks>
        /// A dictionary key is a System.Type, typically a repository type.
        /// A value is a repository factory function
        /// that takes a <see cref="DbContext"/> argument and returns
        /// a repository object. Caller must know how to cast it.
        /// </remarks>
        private readonly IDictionary<Type, Func<ApplicationDbContext, object>> _repositoryFactories;

        /// <summary>
        /// Constructor that initializes with runtime default repository factories
        /// </summary>
        public RepositoryFactories()
        {
            _repositoryFactories = GetDefaultFactories();
        }


        /// <summary>
        /// Return the runtime Code Camper repository factory functions,
        /// each one is a factory for a repository of a particular type.
        /// </summary>
        /// <remarks>
        /// MODIFY THIS METHOD TO ADD CUSTOM CODE CAMPER FACTORY FUNCTIONS
        /// </remarks>
        private IDictionary<Type, Func<ApplicationDbContext, object>> GetDefaultFactories()
        {
            return new Dictionary<Type, Func<ApplicationDbContext, object>>
            {
               {typeof(IRolRepository), _dbContext => new RolRepository(_dbContext)},
               {typeof(IAuthRepository), _dbContext => new AuthRepository(_dbContext)}
            };
        }
        
        /// <summary>
        /// Get the repository factory function for the type.
        /// </summary>
        /// <typeparam name="T">Type serving as the repository factory lookup key.</typeparam>
        /// <returns>The repository function if found, else null.</returns>
        /// <remarks>
        /// The type parameter, T, is typically the repository type 
        /// but could be any type (e.g., an entity type)
        /// </remarks>
        public Func<ApplicationDbContext, object> GetRepositoryFactory<T>()
        {
            _repositoryFactories.TryGetValue(typeof(T), out Func<ApplicationDbContext, object> factory);
            return factory;
        }

        /// <summary>
        /// Get the factory for <see cref="IRepository{T}"/> where T is an entity type.
        /// </summary>
        /// <typeparam name="T">The root type of the repository, typically an entity type.</typeparam>
        /// <returns>
        /// A factory that creates the <see cref="IRepository{T}"/>, given an EF <see cref="DbContext"/>.
        /// </returns>
        /// <remarks>
        /// Looks first for a custom factory in <see cref="_repositoryFactories"/>.
        /// If not, falls back to the <see cref="DefaultEntityRepositoryFactory{T}"/>.
        /// You can substitute an alternative factory for the default one by adding
        /// a repository factory for type "T" to <see cref="_repositoryFactories"/>.
        /// </remarks>
        public Func<ApplicationDbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
        {
            return GetRepositoryFactory<T>() ?? DefaultEntityRepositoryFactory<T>();
        }

        /// <summary>
        /// Default factory for a <see cref="IRepository{T}"/> where T is an entity.
        /// </summary>
        /// <typeparam name="T">Type of the repository's root entity</typeparam>
        protected virtual Func<ApplicationDbContext, object> DefaultEntityRepositoryFactory<T>() where T : class
        {
            return dbContext => new EFRepository<T>(dbContext);
        }


    }
}
