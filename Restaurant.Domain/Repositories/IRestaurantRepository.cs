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
    }
}
