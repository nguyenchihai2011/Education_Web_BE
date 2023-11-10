using EducationAPI.Context;
using EducationAPI.Interfaces;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;

namespace EducationAPI.Implement
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<T> Add(T entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(T entity)
        {
            context.Remove(entity);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T> Update(int id, T entity)
        {

            context.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
