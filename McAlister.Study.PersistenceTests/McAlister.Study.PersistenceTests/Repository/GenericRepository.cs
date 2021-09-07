using McAlister.Study.PersistenceTests.Definitions;
using McAlister.Study.PersistenceTests.Definitions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using dfi = McAlister.Study.PersistenceTests.Definitions.Interfaces;

namespace McAlister.Study.PersistenceTests.Repository
{
    /// <summary>
    /// Returns IQueryable in case Repository wants to use method and take advantage of lazy loading, but IQuerable should not go above Repository
    /// https://blog.ploeh.dk/2012/03/26/IQueryableTisTightCoupling/
    /// </summary>
    /// <typeparam name="C"></typeparam>
    public class GenericRepository<T1> : dfi.IGenericRepository<T1>, IDisposable where T1 : IAggregateRoot
    {
        public string ConnStr => Context.Database.GetDbConnection().ConnectionString;

        public DbContext Context { get; set; }

        public GenericRepository(DbContext context)
        {
            Context = context;
        }

        public virtual T Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            if (predicate != null)
            {
                var x = Context.Set<T>().Where(predicate).SingleOrDefault();
                return x;
            }
            else
            {
                throw new ApplicationException("Predicate value must be passed to Get<T>.");
            }
        }

        public virtual IQueryable<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return Context.Set<T>().Where(predicate);
        }

        public virtual IQueryable<T> GetList<T, TKey>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, TKey>> orderBy) where T : class
        {
            return GetList(predicate).OrderBy(orderBy);
        }

        public virtual IQueryable<T> GetList<T, TKey>(Expression<Func<T, TKey>> orderBy) where T : class
        {
            return GetList<T>().OrderBy(orderBy);
        }


        public virtual IQueryable<T> GetList<T>() where T : class
        {
            return Context.Set<T>();
        }

        public T AddOrUpdate<T>(T entity) where T : class
        {
            //recommend do not use, becasue if model was incomplete, entity will be incomplete and EF will not know and write incomplete entity, thus data can be lost
            throw new NotImplementedException();
        }

        public T Insert<T>(T entity) where T : class
        {
            Context.Set<T>().Add(entity);
            return entity;
        }

        public virtual T GetById<T>(object[] ids) where T : class
        {
            var e = Context.Set<T>().Find(ids);
            return e;
        }

        public virtual void CopyValues<T>(T modifiedEntity, T existingEntity) where T : class
        {
            var attachedEntry = Context.Entry(existingEntity);
            attachedEntry.CurrentValues.SetValues(modifiedEntity);
        }

        public virtual T Update<T>(T entity) where T : class
        {
            //assumes entity is tracked because you just retrieved it and updated its values
            var entry = Context.Entry<T>(entity);
            entry.State = EntityState.Modified;
            return entity;
        }

        public virtual void Delete<T>(T entity) where T : class
        {
            if (entity != null)
            {
                var entry = Context.Entry(entity);
                if (entry.State == EntityState.Detached)
                    Context.Set<T>().Attach(entity);
                Context.Set<T>().Remove(entity);
            }
        }

        public virtual void Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var lst = GetList<T>(predicate);
            DbSet<T> objectSet = Context.Set<T>();
            foreach (var entity in lst)
            {
                objectSet.Remove(entity);
            }
        }

        public void DeleteRange<T>(IEnumerable<T> lst) where T : class
        {
            Context.Set<T>().RemoveRange(lst);
        }

        public virtual OperationStatus ExecuteCommand(string cmdText, params object[] parameters)
        {
            var opStatus = new OperationStatus { Status = true };

            try
            {
                opStatus.RecordsAffected = Context.Database.ExecuteSqlRaw(cmdText, parameters);
            }
            catch (Exception exp)
            {
                OperationStatus.CreateFromException("Error executing store command: ", exp);
            }
            return opStatus;
        }

        public int SubmitChanges()
        {
            int result = 0;
            try
            {
                result = this.Context.SaveChanges();
            }
            catch (DbUpdateException dbUEx)
            {
                TrackEntityChanges();
                var sqlException = dbUEx.GetBaseException();
                if (sqlException != null)
                    throw sqlException;
                throw dbUEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<int> SubmitChangesAsync()
        {
            int result = 0;
            try
            {
                result = await this.Context.SaveChangesAsync();
            }
            catch (DbUpdateException dbUEx)
            {
                TrackEntityChanges();
                var sqlException = dbUEx.GetBaseException();
                if (sqlException != null)
                    throw sqlException;
                throw dbUEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        private void TrackEntityChanges()
        {
            foreach (EntityEntry entry in this.Context.ChangeTracker.Entries())
            {
                TrackEntityChanges(entry);
            }
        }

        private void TrackEntityChanges(EntityEntry entry)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Modified:
                case EntityState.Deleted:
                    entry.Reload();
                    break;
            }
        }

        void IDisposable.Dispose()
        {
            //TODO Consider implemneting dispose pattern
            Context.Database.CloseConnection();
            Context.Dispose();
        }
    }
}
