namespace CarRentalSystem.Infrastructure.Common
{
    using System.Linq;
    using Application.Common;

    public static class QueryableExtensions
    {
        public static IOrderedQueryable<TEntity> Sort<TEntity>(
            this IQueryable<TEntity> entities, SortOrder<TEntity> sortOrder) 
            => SortOrder<TEntity>.Descending == sortOrder.Order
                ? entities.OrderByDescending(sortOrder.ToExpression())
                : entities.OrderBy(sortOrder.ToExpression());
    }
}