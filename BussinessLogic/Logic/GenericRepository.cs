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
        
        public Task<int> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChangesAsync();
        }
        
        public Task<int> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChangesAsync();
        }
       
        public Task<int> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChangesAsync();
        }

        public async Task<T> GetById(string id)
        {
            return await _context.Set<T>().FindAsync(id);       
        }
    }
}
