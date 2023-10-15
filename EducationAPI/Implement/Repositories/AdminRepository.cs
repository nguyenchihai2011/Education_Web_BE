﻿using EducationAPI.Context;
using EducationAPI.Entities;
using EducationAPI.Interfaces.Repositories;

namespace EducationAPI.Implement.Repositories
{
    public class AdminRepository : GenericRepository<AdminEntity>, IAdminRepository
    {
        public AdminRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
