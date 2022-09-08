using BussinessLogic.Data;
using Core.Entities;
using Core.Interface;

namespace BussinessLogic.Logic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : CommonProperties
    {
        private readonly ContentDbContext _context;
        public GenericRepository(ContentDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add new entity to database
        /// </summary>
        /// <param name="entity"></param>
        public Task<int> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChangesAsync();
        }
        
        /// <summary>
        /// Update entity in database
        /// </summary>
        /// <param name="entity"></param>
        public Task<int> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete entity from database
        /// </summary>
        /// <param name="entity"></param>
        public Task<int> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChangesAsync();
        }
    }
}
