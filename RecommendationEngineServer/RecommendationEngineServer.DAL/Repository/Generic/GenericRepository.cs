using Microsoft.EntityFrameworkCore;

namespace RecommendationEngineServer.DAL.Repository.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbContext context;

        public GenericRepository(DbContext context)
        {
            this.context = context;
        }
        public async Task<T> Create(T model)
        {
            try
            {
                await context.Set<T>().AddAsync(model);
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public async Task<IQueryable<T>> GetAll()
        {
            return await Task.FromResult(context.Set<T>());
        }

        public async Task<T> GetById(object id)
        {
            return await context.Set<T>().FindAsync(id);
        }
        public async Task<bool> Delete(int id)
        {
            var entity = await GetById(id);
            try
            {
                context.Set<T>().Remove(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
