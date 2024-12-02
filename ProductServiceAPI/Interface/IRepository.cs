using ProductServiceAPI.Entities;

namespace ProductServiceAPI.Interface
{
    /// <summary>
    /// Интерфейс базового репозитория.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>  where T : Product
    {
        /// <summary>
        /// Получает сущность по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<T>> GetAsync(int id, CancellationToken cancellation);
        /// <summary>
        /// Добавляет сущность в БД.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task AddAsync(T entity, CancellationToken cancellation);
        /// <summary>
        /// Получает все сущности.
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<List<T>> GetAllAsync(CancellationToken cancellation);
        /// <summary>
        /// Обновляет сущность.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity, CancellationToken cancellation);
        /// <summary>
        /// Удаление сущности.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<T> DeleteAsync(int id, CancellationToken cancellation);
    }
}
