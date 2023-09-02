using DB.Interfaces;
using Delivery_App_Code_Challenge.DB.Models;
using Microsoft.EntityFrameworkCore;
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



        public Task<(bool, string?)> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<(bool, string?)> AddRangeAsync(List<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<(bool, string?)> DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool asNoTracking = true)
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

        public Task<T?> GetSingleOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool asNoTracking = true)
        {
            throw new NotImplementedException();
        }

        public (bool, string?) Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public (bool, string?) RemoveRange(List<T> entities)
        {
            throw new NotImplementedException();
        }

        public (bool, string?) Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
