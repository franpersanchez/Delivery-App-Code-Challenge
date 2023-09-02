using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DB.Interfaces
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Gets all the entities of <see cref="IRepository{T}"></see> that match the given expression
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition (optional)</param>
        /// <param name="orderBy">An expression to be applied to order the entities (optional)</param>
        /// <param name="include">An expression to be applied to include child entities (optional)</param>
        /// <param name="asNoTracking">Boolean to use AsNoTracking when getting the entities. Defaults to true</param>
        /// <returns>The entity with the given id or null if it is not found></see></returns>
        /// <remarks>This method defaults to no tracking query</remarks>
        /// <example>
        /// Usage: 
        /// <code>
        /// // After injecting the repository into _operationLogRepository for data class OperationLog
        /// List<OperationLog> opLogs = await _operationLogRepository.GetAllAsync(predicate: op => op.Name == "Unnamed"
        ///                                                          , orderBy: op => op.OrderBy(op => op.StartDateTime)
        ///                                                          , include: op => op.Include(op => op.MachiningProcess)
        ///                                                          , asNoTracking: true);
        /// </code>
        /// </example>
        public Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool asNoTracking = true);

        /// <summary>
        /// Gets the single entity of <see cref="IRepository{T}"></see> tha matches the given predicate. Use this method to implement GetById.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition (optional)</param>
        /// <param name="include">An expression to be applied to include child entities (optional)</param>
        /// <param name="asNoTracking">Boolean to use AsNoTracking when getting the entities. Defaults to true</param>
        /// <returns>The entity with the given id or null if it is not found></see></returns>
        /// <remarks>This method defaults to no tracking query</remarks>
        /// <example>
        /// Usage: 
        /// <code>
        /// // After injecting the repository into _operationLogRepository for data class OperationLog
        /// List<OperationLog> opLogs = await _operationLogRepository.GetSingleOrDefaultAsync(predicate: op => op.Id == "847ccec3-3bea-43a6-997f-8497d6da3412"
        ///                                                          , include: op => op.Include(op => op.MachiningProcess)
        ///                                                          , asNoTracking: true);
        /// </code>
        /// </example>
        public Task<T?> GetSingleOrDefaultAsync(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool asNoTracking = true);


        /// <summary>
        /// Persist a new entity of <see cref="IRepository{T}"></see>
        /// </summary>
        /// <param name="entity">Entity to be added</param>
        /// <returns>A tuple (bool, string). The bool represents the call success and the string any possible message.</returns>
        public Task<(bool, string?)> AddAsync(T entity);

        /// <summary>
        /// Persist a new a collection of entities of <see cref="IRepository{T}"></see>
        /// </summary>
        /// <param name="entities">List of entities to be added</param>
        /// <returns>A tuple (bool, string). The bool represents the call success and the string any possible message.</returns>
        public Task<(bool, string?)> AddRangeAsync(List<T> entities);

        /// <summary>
        /// Updates an existing entity of <see cref="IRepository{T}"></see>
        /// </summary>
        /// <param name="entity">Updated entity</param>
        /// <returns>A tuple (bool, string). The bool represents the call success and the string any possible message.</returns>
        public (bool, string?) Update(T entity);

        /// <summary>
        /// Removes an existing entity of <see cref="IRepository{T}"></see>
        /// </summary>
        /// <param name="entity">Entity to be removed</param>
        /// <returns>A tuple (bool, string). The bool represents the call success and the string any possible message.</returns>
        public (bool, string?) Remove(T entity);

        /// <summary>
        /// Removes a collection of entities of <see cref="IRepository{T}"></see>
        /// </summary>
        /// <param name="entities">List of entities to be removed</param>
        /// <returns>A tuple (bool, string). The bool represents the call success and the string any possible message.</returns>
        public (bool, string?) RemoveRange(List<T> entities);

        /// <summary>
        /// Deletes all entities of type T.
        /// </summary>
        /// <returns>A tuple (bool, string). The bool represents the call success, and the string contains any possible error message.</returns>
        public Task<(bool, string?)> DeleteAllAsync();
    }
}
