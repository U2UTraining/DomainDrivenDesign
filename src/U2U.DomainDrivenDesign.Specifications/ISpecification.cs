//namespace U2U.DomainDrivenDesign.Specifications;

///// <summary>
///// A ISpecification represents the filter (Where) and includes of a query,
///// to be used with an DbContext for finding entities.
///// </summary>
///// <typeparam name="T">The entity class' type.</typeparam>
//public interface ISpecification<T>
//{
//  /// <summary>
//  /// Get List of Items matching specification
//  /// </summary>
//  /// <typeparam name="T"></typeparam>
//  /// <param name="table"></param>
//  /// <returns></returns>
//  ValueTask<IEnumerable<T>> ListAsync(IQueryable<T> table);

//  /// <summary>
//  /// Get optional single instance matching specification
//  /// </summary>
//  /// <typeparam name="T"></typeparam>
//  /// <param name="table"></param>
//  /// <returns></returns>
//  ValueTask<T?> SingleAsync(IQueryable<T> table);

//  /// <summary>
//  /// True if the instance passes the specification
//  /// </summary>
//  /// <param name="t">The instance to test</param>
//  /// <returns></returns>
//  bool Test(in T t);

//  ///// <summary>
//  ///// Return a new specification that includes a list.
//  ///// </summary>
//  ///// <param name="includes"></param>
//  ///// <returns></returns>
//  //ISpecification<T> Include(IEnumerable<Expression<Func<T, object>>> includes);

//  ///// <summary>
//  ///// Return a new specification that includes the extra entity.
//  ///// </summary>
//  ///// <param name="include"></param>
//  ///// <returns></returns>
//  //ISpecification<T> Include(Expression<Func<T, object>> include);
//}
