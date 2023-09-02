using DB.Interfaces;
using Delivery_App_Code_Challenge.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DB.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly IDbContextFactory<DeliveryAppContext> _dbContextFactory;

        //Constructor
        public Repository(IDbContextFactory<DeliveryAppContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <summary>
        /// Get a list with all the entities
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>,
                                                IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T,
                                                object>>? include = null, bool asNoTracking = true)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var entities = context.Set<T>();

            IQueryable<T> query = entities;

            if (asNoTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) query = orderBy(query);

            var result = await query.ToListAsync();

            await context.DisposeAsync();

            return result;
        }

        /// <summary>
        /// Get one single entity or a default value
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        public async Task<T?> GetSingleOrDefaultAsync(Expression<Func<T, bool>>? predicate = null,
                                            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                            bool asNoTracking = true)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var entities = context.Set<T>();

            IQueryable<T> query = entities;

            if (asNoTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return await query.SingleOrDefaultAsync();
        }


        /// <summary>
        /// Add an entity to a table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<(bool, string?)> AddAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            using var context = await _dbContextFactory.CreateDbContextAsync();

            bool rowsChanged = false;
            string? errorMessage = null;
            try
            {
                await context.AddAsync(entity);
                rowsChanged = await context.SaveChangesAsync() > 0;
            }
            catch (Exception e) { errorMessage = e.Message; }
            finally { await context.DisposeAsync(); }

            return (rowsChanged, errorMessage);
        }

        /// <summary>
        /// Add a range of entities to a table
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<(bool, string?)> AddRangeAsync(List<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            using var context = await _dbContextFactory.CreateDbContextAsync();

            var rowsChanged = false;
            string? errorMessage = null;
            try
            {
                await context.AddRangeAsync(entities);
                rowsChanged = await context.SaveChangesAsync() > 0;
            }
            catch (Exception e) { errorMessage = e.Message; }
            finally { await context.DisposeAsync(); }

            return (rowsChanged, errorMessage);
        }

        /// <summary>
        /// Delete all entities from a given repository
        /// </summary>
        /// <returns></returns>
        public async Task<(bool, string?)> DeleteAllAsync()
        {
            using var context = _dbContextFactory.CreateDbContext();

            string? errorMessage = null;

            try
            {
                var allEntities = await GetAllAsync(asNoTracking: false); // Retrieve all entities of type T
                context.RemoveRange(allEntities); // Remove all entities from the context
                await context.SaveChangesAsync(); // Save changes to delete the entities in the database
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }

            return (errorMessage == null, errorMessage);
        }

        /// <summary>
        /// Remove an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public (bool, string?) Remove(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            using var context = _dbContextFactory.CreateDbContext();

            bool rowsChanged = false;
            string? errorMessage = null;

            try
            {
                context.Remove(entity);
                rowsChanged = context.SaveChanges() > 0;
            }
            catch (Exception e) { errorMessage = e.Message; }
            finally { context.Dispose(); }

            return (rowsChanged, errorMessage);
        }

        /// <summary>
        /// Remove a range of entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public (bool, string?) RemoveRange(List<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            using var context = _dbContextFactory.CreateDbContext();

            bool rowsChanged = false;
            string? errorMessage = null;

            try
            {
                context.RemoveRange(entities);
                rowsChanged = context.SaveChanges() > 0;
            }
            catch (Exception e) { errorMessage = e.Message; }
            finally { context.Dispose(); }

            return (rowsChanged, errorMessage);
        }


        /// <summary>
        /// Update a certain entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public (bool, string?) Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            using var context = _dbContextFactory.CreateDbContext();

            var rowsChanged = false;
            string? errorMessage = null;

            try
            {
                context.Update(entity);
                rowsChanged = context.SaveChanges() > 0;
            }
            catch (Exception e) { errorMessage = e.Message; }
            finally { context.Dispose(); }

            return (rowsChanged, errorMessage);
        }

    }









}
