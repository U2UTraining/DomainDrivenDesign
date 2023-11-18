namespace U2U.DomainDrivenDesign.Specifications;
internal static class DbContextExtensions
{
  public static IQueryable<T> BuildQueryable<T>(this IQueryable<T> table, Specification<T> specification)
  where T : class
    => specification.BuildQueryable(table);

  public static async ValueTask<IEnumerable<T>> ListAsync<T>(this IQueryable<T> table, Specification<T> specification)
  where T : class
    => await specification.ToListAsync(table);

  public static async ValueTask<T?> SingleAsync<T>(this IQueryable<T> table, Specification<T> specification)
  where T : class
    => await specification.SingleAsync(table);
}
