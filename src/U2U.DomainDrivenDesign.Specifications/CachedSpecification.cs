namespace U2U.DomainDrivenDesign.Specifications;
public class CachedSpecification<T, K>
    : Specification<T>, IEquatable<CachedSpecification<T, K>>
  where T : class
{
  public CachedSpecification(Expression<Func<T, bool>> criteria, IEnumerable<Expression<Func<T, object>>> includes, TimeSpan cacheDuration, K key)
  : base(criteria, includes)
  {
    CacheDuration = cacheDuration;
    Key = key;
  }

  public CachedSpecification(Expression<Func<T, bool>> criteria, TimeSpan cacheDuration, K key)
  : base(criteria)
  {
    CacheDuration = cacheDuration;
    Key = key;
  }

  public K Key { get; }

  public TimeSpan CacheDuration { get; set; }

  public bool Equals(CachedSpecification<T, K>? other)
  {
    if (object.ReferenceEquals(this, other))
    {
      return true;
    }
    return other is not null
&& EqualityComparer<K>.Default.Equals(Key, other.Key)
      && (CacheDuration == other.CacheDuration)
      && base.Equals(other);
  }

  public override bool Equals(object? obj)
  {
    if (object.ReferenceEquals(this, obj))
    {
      return true;
    }
    if (GetType() == obj?.GetType())
    {
      CachedSpecification<T, K> other = (CachedSpecification<T, K>)obj;
      return Equals(other);
    }
    return false;
  }

  public override int GetHashCode()
    => HashCode.Combine(Criteria, CacheDuration, Key);
}
