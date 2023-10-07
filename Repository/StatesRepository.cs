using Microsoft.EntityFrameworkCore;
using World.Api.Data;
using World.Api.Models;
using World.Api.Repository.IRepository;

namespace World.Api.Repository
{
    public class StatesRepository : GenericRepository<States>,IStatesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StatesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Update(States entity)
        {
            _dbContext.State.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        //public async Task Create(States entity)
        //{
        //    await _dbContext.State.AddAsync(entity);
        //    await Save();
        //}

        //public async Task Delete(States entity)
        //{
        //    _dbContext.State.Remove(entity);
        //    await Save();
        //}

        //public async Task<List<States>> GetAll()
        //{
        //    List<States> states = await _dbContext.State.ToListAsync();
        //    return states;
        //}

        //public async Task<States> GetById(int id)
        //{
        //    States state = await _dbContext.State.FindAsync(id);
        //    return state;
        //}

        //public bool IsStateExists(string name)
        //{
        //    var result = _dbContext.State.AsQueryable().Where(x=> x.Name.ToLower().Trim() == name.ToLower().Trim()).Any();
        //    return result;
        //}

        //public async Task Save()
        //{
        //    await _dbContext.SaveChangesAsync();
        //}
    }
}
