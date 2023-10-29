using EducationAPI.Context;
using EducationAPI.Entities;
using EducationAPI.Interfaces.Repositories;

namespace EducationAPI.Implement.Repositories
{
    public class NotifycationRepository : GenericRepository<Notifycation>, INotifycationRepository
    {
        public NotifycationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
