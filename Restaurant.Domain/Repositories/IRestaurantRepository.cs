using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Repositories
{
    public interface IRestaurantRepository
    {
        public Task<IEnumerable<Restaurantt>> GetAllAsync();
        public Task<Restaurantt?> GetById(int id);
        public Task<int> Create(Restaurantt restaurant); 
        public Task Delete(Restaurantt entity);
        public Task Update(Restaurantt entity);
        public Task<(IEnumerable<Restaurantt>, int)> GetAllMatching(string? searchPhrase,
                                                                    int pageSize,
                                                                    int pageNumber,
                                                                    string? sortBy,
                                                                    SortDirection? sortDirection);
        public Task SaveChanges();
    }
}
