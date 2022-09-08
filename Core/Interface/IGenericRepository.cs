using Core.Entities;

namespace Core.Interface
{
    public interface IGenericRepository<T> where T : CommonProperties
    {
        Task<int> Add(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
    }
}
