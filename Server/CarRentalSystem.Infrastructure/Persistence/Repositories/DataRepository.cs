namespace CarRentalSystem.Infrastructure.Persistence.Repositories
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Contracts;
    using CarRentalSystem.Domain.Common;

    internal abstract class DataRepository<TEntity> : IRepository<TEntity> 
        where TEntity : class, IAggregateRoot
    {
        protected DataRepository(CarRentalDbContext db) => Data = db;

        public CarRentalDbContext Data { get; }

        public IQueryable<TEntity> All() => Data.Set<TEntity>();

        public async Task Save(TEntity entity, CancellationToken cancellationToken = default)
        {
            Data.Update(entity);
            await Data.SaveChangesAsync(cancellationToken);
        }
    }
}