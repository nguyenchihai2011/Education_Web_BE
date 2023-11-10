using EducationAPI.Context;
using EducationAPI.Entities;
using EducationAPI.Interfaces.Repositories;

namespace EducationAPI.Implement.Repositories
{
    public class CartDetailsRepository : GenericRepository<CartDetails>, ICartDetailsRepository
    {
        public CartDetailsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
