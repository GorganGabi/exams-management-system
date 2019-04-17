using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EMS.Domain
{
    public interface IRepository
    {
        IQueryable<TEntity> GetAll<TEntity>()
            where TEntity : Entity;

        Task<TEntity> FindByIdAsync<TEntity>(Guid id)
            where TEntity : Entity;

        Task TryUpdateModelAsync<TEntity>(TEntity entityToUpdate, TEntity updatedEntity)
            where TEntity : IUpdatable<TEntity>;

        Task AddNewAsync<TEntity>(TEntity entity)
            where TEntity : Entity;

        Task SaveAsync();

        Task RemoveAsync<TEntity>(TEntity entity)
            where TEntity : Entity;
        
    }
}
