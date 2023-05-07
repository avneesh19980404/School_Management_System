using School.Core.Data;
using School.Model.Entity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace School.Service.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {

        DatabaseFacade Database { get; }
        /// <summary>
        /// Returns the queryable entity set for the given type {T}.
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// Returns an untracked queryable entity set for the given type {T}.
        /// The entities returned will not be cached in the object context thus increasing performance.
        /// </summary>
        IQueryable<T> TableUntracked { get; }

        /// <summary>
        /// Provides access to the entities currently being tracked by the context and have not been marked as deleted
        /// </summary>
        ICollection<T> Local { get; }

        /// <summary>
        /// Gets an entity by id from the database or the local change tracker.
        /// </summary>
        /// <param name="id">The id of the entity. This can also be a composite key.</param>
        /// <returns>The resolved entity</returns>
        T GetById(object id);
         /// <summary>
        /// Marks an entity instance to be saved to the store and saves immediately if <see cref="AutoCommitEnabled"/> is <c>true</c>
        /// </summary>
        /// <param name="entity">An entity instance that should be saved to the database.</param>
        /// <remarks>Implementors should delegate this to the current <see cref="IDbContext" /></remarks>
        void Insert(T entity);

        /// <summary>
        /// Marks an entity instance to be saved to the store and saves immediately if <see cref="AutoCommitEnabled"/> is <c>true</c>.
        /// </summary>
        /// <param name="entity">An entity instance that should be saved to the database.</param>
        /// <remarks>Implementors should delegate this to the current <see cref="IDbContext" /></remarks>
        Task InsertAsync(T entity);

        /// <summary>
        /// Marks multiple entities to be saved to the store.
        /// </summary>
        /// <param name="entities">The list of entity instances to be saved to the database</param>
        /// <param name="batchSize">The number of entities to insert before saving to the database (if <see cref="AutoCommitEnabled"/> is true)</param>
        void InsertRange(IEnumerable<T> entities, int batchSize = 100);

        IEnumerable<T> GetAll();
        /// <summary>
        /// Marks multiple entities to be saved to the store and saves immediately if <see cref="AutoCommitEnabled"/> is <c>true</c>.
        /// </summary>
        /// <param name="entities">The list of entity instances to be saved to the database</param>
        /// <param name="batchSize">The number of entities to insert before saving to the database (if <see cref="AutoCommitEnabled"/> is true)</param>
        Task InsertRangeAsync(IEnumerable<T> entities, int batchSize = 100);

        /// <summary>
        /// Marks the changes of an existing entity to be saved to the store and saves immediately if <see cref="AutoCommitEnabled"/> is <c>true</c>.
        /// </summary>
        /// <param name="entity">An instance that should be updated in the database.</param>
        /// <remarks>Implementors should delegate this to the current <see cref="IDbContext" /></remarks>
        void Update(T entity);

        /// <summary>
        /// Marks the changes of an existing entity to be saved to the store and saves immediately if <see cref="AutoCommitEnabled"/> is <c>true</c>.
        /// </summary>
        /// <param name="entity">An instance that should be updated in the database.</param>
        /// <remarks>Implementors should delegate this to the current <see cref="IDbContext" /></remarks>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Marks an existing entity to be deleted from the store and saves immediately if <see cref="AutoCommitEnabled"/> is <c>true</c>.
        /// </summary>
        /// <param name="entity">An entity instance that should be deleted from the database.</param>
        /// <remarks>Implementors should delegate this to the current <see cref="IDbContext" /></remarks>
        void Delete(T entity);

        IQueryable<T> Expand(IQueryable<T> query, string path);
        IQueryable<T> Expand<TProperty>(IQueryable<T> query, Expression<Func<T, TProperty>> path);


        /// <summary>
        /// Gets or sets a value indicating whether database write operations
        /// such as insert, delete or update should be committed immediately.
        /// </summary>
		/// <remarks>
		/// Set this to <c>true</c> or <c>false</c> to supersede the global <c>AutoCommitEnabled</c>
		/// on <see cref="IDbContext"/> level for this repository instance only.
		/// </remarks>
        bool? AutoCommitEnabled { get; set; }
    }
}
