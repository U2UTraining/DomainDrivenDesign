using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2U.DomainDrivenDesign.Specifications;
public static class SpecificationExtensions
{
  public static Specification<T> AsCached<T, K>(this Specification<T> spec, TimeSpan duration, K key)
    where T : class//, IEntity
  => new CachedSpecification<T, K>(spec.Criteria, spec.Includes, duration, key);

  public static Specification<T> Including<T>(this Specification<T> spec, Expression<Func<T, object>> include)
    where T : class//, IEntity
    => spec.Include(include);

  public static Specification<T> And<T>(this Specification<T> left, Specification<T> right)
    where T : class//, IEntity
    => And<T>(left, right.Criteria);

  public static Specification<T> And<T>(this Specification<T> left, Expression<Func<T, bool>> rightExpression)
    where T : class//, IEntity
  {
    Expression<Func<T, bool>> leftExpression = left.Criteria;

    var visitor = new SwapVisitor(leftExpression.Parameters[0], rightExpression.Parameters[0]);
    BinaryExpression lazyAnd = Expression.AndAlso(visitor.Visit(leftExpression.Body)!, rightExpression.Body);
    var and = Expression.Lambda<Func<T, bool>>(lazyAnd, rightExpression.Parameters);
    return new Specification<T>(and);
  }

  public static Specification<T> Or<T>(this Specification<T> left, Specification<T> right)
    where T : class//, IEntity
    => Or<T>(left, right.Criteria);


  public static Specification<T> Or<T>(this Specification<T> left, Expression<Func<T, bool>> rightExpression)
    where T : class//, IEntity
  {
    Expression<Func<T, bool>> leftExpression = left.Criteria;
    var visitor = new SwapVisitor(leftExpression.Parameters[0], rightExpression.Parameters[0]);
    BinaryExpression lazyOr = Expression.OrElse(visitor.Visit(leftExpression.Body)!, rightExpression.Body);
    var or = Expression.Lambda<Func<T, bool>>(lazyOr, rightExpression.Parameters);
    return new Specification<T>(or);
  }

  public static Specification<T> Not<T>(this Specification<T> left)
    where T : class//, IEntity
  {
    Expression<Func<T, bool>> leftExpression = left.Criteria;
    UnaryExpression notExpression = Expression.Not(leftExpression.Body);
    var not = Expression.Lambda<Func<T, bool>>(notExpression, leftExpression.Parameters);
    return new Specification<T>(not);
  }

  private class SwapVisitor : ExpressionVisitor
  {
    private readonly Expression from, to;

    public SwapVisitor(Expression from, Expression to)
    {
      this.from = from;
      this.to = to;
    }

    public override Expression? Visit(Expression? node)
      => node == this.from ? this.to : base.Visit(node);
  }
}

