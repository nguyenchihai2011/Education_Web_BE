namespace EducationAPI.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<List<T>> GetAllAsync();
        public Task<T> GetById(int id);
        public Task<T> Add(T entity);
        public Task<T> Update(int id, T entity);
        public Task Delete(T entity);
    }
}
