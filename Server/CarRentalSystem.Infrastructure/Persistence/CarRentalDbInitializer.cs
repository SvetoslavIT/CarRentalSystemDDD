namespace CarRentalSystem.Infrastructure.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using CarRentalSystem.Domain.Common;
    using Microsoft.EntityFrameworkCore;

    internal class CarRentalDbInitializer : IInitializer
    {
        private readonly CarRentalDbContext _db;
        private readonly IEnumerable<IInitialData> _initialDataProviders;

        public CarRentalDbInitializer(
            CarRentalDbContext db,
            IEnumerable<IInitialData> initialDataProviders)
        {
            _db = db;
            _initialDataProviders = initialDataProviders;
        }

        public void Initialize()
        {
            _db.Database.Migrate();

            foreach (var initialDataProvider in _initialDataProviders)
            {
                if (!DataSetIsEmpty(initialDataProvider.EntityType))
                {
                    continue;
                }

                var data = initialDataProvider.GetData();

                foreach (var entity in data)
                {
                    _db.Add(entity);
                }
            }

            _db.SaveChanges();
        }

        private bool DataSetIsEmpty(Type type)
        {
            var setMethod = GetType()
                .GetMethod(nameof(GetSet), BindingFlags.Instance | BindingFlags.NonPublic)!
                .MakeGenericMethod(type);

            var set = setMethod.Invoke(this, Array.Empty<object>());

            var countMethod = typeof(Queryable)
                .GetMethods()
                .First(m => m.Name == nameof(Queryable.Count) && m.GetParameters().Length == 1)
                .MakeGenericMethod(type);

            var result = (int)countMethod.Invoke(null, new[] { set })!;

            return result == 0;
        }

        private DbSet<TEntity> GetSet<TEntity>()
            where TEntity : class
            => _db.Set<TEntity>();
    }
}
