using Core.Entities;

namespace Core.Interface
{
    public interface IGenericRepository<T> where T : CommonProperties
    {
        /// <summary>
        /// Add new entity to database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// 1 = Success
        /// 0 = Fail
        /// </returns>
        Task<int> Add(T entity);

        /// <summary>
        /// Update entity in database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// 1 = Success
        /// 0 = Fail
        /// </returns>
        Task<int> Update(T entity);

        /// <summary>
        /// Delete entity from database
        /// </summary>
        /// <param name="entity"></param>
        ///<returns>
        /// 1 = Success
        /// 0 = Fail
        /// </returns>
        Task<int> Delete(T entity);

        /// <summary>
        /// Get entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Entity
        /// </returns>
        Task<T> GetById(string id);
    }
}
