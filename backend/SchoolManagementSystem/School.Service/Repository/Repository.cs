using School.Core.Data;
using School.Core.Extension;
using School.Core.Helper;
using School.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EfState = Microsoft.EntityFrameworkCore.EntityState;

namespace School.Service.Repository
{
    public partial class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private DbSet<T> _entities;

        public Repository(AppDbContext context)
        {
            this._context = context;
            _entities = context.Set<T>();
            this.AutoCommitEnabled = true;
        }

        #region interface members   
        public virtual DatabaseFacade Database
        {
            get {
                return this.Context.Database;
            }
        
        }
        public virtual IQueryable<T> Table
        {
            get
            {
               return this._entities;
            }
        }

        public virtual IQueryable<T> TableUntracked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this._entities.AsNoTracking();
        }

        public virtual ICollection<T> Local
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this._entities.Local;
        }
        public IEnumerable<T> GetAll()
        {
            return this._entities.AsEnumerable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual T GetById(object id)
        {
            return this._entities.Find(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual ValueTask<T> GetByIdAsync(object id)
        {
            return _entities.FindAsync(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual EntityEntry<T> Attach(T entity)
        {
            return this._entities.Attach(entity);
        }

        public virtual void Insert(T entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException("reference not found");
            }

            this._entities.Add(entity);
            if (this.AutoCommitEnabledInternal)
                _context.SaveChanges();
        }

        public virtual async Task InsertAsync(T entity)
        {
            Guard.NotNull(entity, nameof(entity));

            this._entities.Add(entity);
            if (this.AutoCommitEnabledInternal)
                await _context.SaveChangesAsync();
        }

        public virtual void InsertRange(IEnumerable<T> entities, int batchSize = 100)
        {
            Guard.NotNull(entities, nameof(entities));

            try
            {
                foreach (var batch in entities.Slice(batchSize <= 0 ? int.MaxValue : batchSize))
                {
                    this._entities.AddRange(batch);
                    if (this.AutoCommitEnabledInternal)
                        _context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public virtual async Task InsertRangeAsync(IEnumerable<T> entities, int batchSize = 100)
        {
            Guard.NotNull(entities, nameof(entities));

            try
            {
                foreach (var batch in entities.Slice(batchSize <= 0 ? int.MaxValue : batchSize))
                {
                    this._entities.AddRange(batch);
                    if (this.AutoCommitEnabledInternal)
                        await _context.SaveChangesAsync();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public virtual void Update(T entity)
        {
            Guard.NotNull(entity, nameof(entity));

            ChangeStateToModifiedIfApplicable(entity);
            if (this.AutoCommitEnabledInternal)
                _context.SaveChanges();
        }
        private void ChangeStateToModifiedIfApplicable(T entity)
        {
            var entry = _context.Entry(entity);

            if (entry.State == EfState.Detached)
            {
                // Entity was detached before or was explicitly constructed.
                // This unfortunately sets all properties to modified.
                entry.State = EfState.Modified;
            }
            else if (entry.State == EfState.Unchanged)
            {
                // We simply do nothing here, because it is ensured now that DetectChanges()
                // gets implicitly called prior SaveChanges().

                //if (this.AutoCommitEnabledInternal && !ctx.Configuration.AutoDetectChangesEnabled)
                //{
                //	_context.DetectChanges();
                //}
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            Guard.NotNull(entity, nameof(entity));

            ChangeStateToModifiedIfApplicable(entity);
            if (this.AutoCommitEnabledInternal)
                await _context.SaveChangesAsync();
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            Guard.NotNull(entities, nameof(entities));

            foreach (var entity in entities)
            {
                ChangeStateToModifiedIfApplicable(entity);
            }

            if (this.AutoCommitEnabledInternal)
            {
                _context.SaveChanges();
            }
        }
        public virtual void Delete(T entity)
        {
            Guard.NotNull(entity, nameof(entity));
            _context.Remove(entity);
            if (this.AutoCommitEnabledInternal)
                _context.SaveChanges();
        }

        [Obsolete("Use the extension method from 'School.Data' instead")]
        public IQueryable<T> Expand(IQueryable<T> query, string path)
        {
            Guard.NotNull(query, "query");
            return query.Include(path);
        }

        [Obsolete("Use the extension method from 'School.Data' instead")]
        public IQueryable<T> Expand<TProperty>(IQueryable<T> query, Expression<Func<T, TProperty>> path)
        {
            Guard.NotNull(query, "query");
            Guard.NotNull(path, "path");

            return query.Include(path);
        }
        public virtual AppDbContext Context => _context;

        public bool? AutoCommitEnabled { get; set; }

        private bool AutoCommitEnabledInternal => this.AutoCommitEnabled ?? false;

        #endregion

    }
}
