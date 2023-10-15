﻿using EducationAPI.Context;
using EducationAPI.Entities;
using EducationAPI.Interfaces.Repositories;
using EducationAPI.Models;

namespace EducationAPI.Implement.Repositories
{
    public class QuizRepository : GenericRepository<QuizEntity>, IQuizRepository
    {
        public QuizRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
