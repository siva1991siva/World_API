using System.Linq.Expressions;
using World.Api.Models;

namespace World.Api.Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task Create(T entity);        
        Task Delete(T entity);
        Task Save();
        //bool IsCountryExsits(string name);
        bool IsRecordExsits(Expression<Func<T,bool>>condition);
    }
}
