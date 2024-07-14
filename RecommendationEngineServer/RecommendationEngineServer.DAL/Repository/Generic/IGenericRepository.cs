namespace RecommendationEngineServer.DAL.Repository.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Create(T model);
        Task<IQueryable<T>> GetAll();
        Task<T> GetById(object id);
        Task<bool> Delete(int id);
    }
}
