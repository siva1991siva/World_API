using World.Api.Migrations;
using World.Api.Models;

namespace World.Api.Repository.IRepository
{
    public interface IStatesRepository : IGenericRepository<States> 
    {
        Task Update(States entity);
        //Task<List<States>> GetAll();
        //Task<States> GetById(int id);
        //Task Create(States entity);
        //Task Delete(States entity);
        //Task Save();
        //bool IsStateExists(string name);
    }
}
