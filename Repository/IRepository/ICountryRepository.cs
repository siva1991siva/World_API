using World.Api.Models;

namespace World.Api.Repository.IRepository
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        //Task<List<Country>> GetAll();
        //Task<Country> GetById(int id);
        //Task Create(Country entity);
        //Task Delete(Country entity);
        //Task Save();
        //bool IsCountryExsits(string name);
        Task Update(Country entity);
    }
}
