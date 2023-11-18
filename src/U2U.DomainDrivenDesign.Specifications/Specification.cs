namespace U2U.DomainDrivenDesign.Specifications;

/// <summary>
/// Implementation for ISpecification<typeparamref name="T"/>
/// </summary>
/// <typeparam name="T">The entity class' type.</typeparam>
public class Specification<T> : /*ISpecification<T>, */IEquatable<Specification<T>>
 where T : class
{
  public Specification(Expression<Func<T, bool>> criteria)
    : this(criteria, ImmutableList<Expression<Func<T, object>>>.Empty)
  { }

  public Specification(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> include)
    : this(criteria, new List<Expression<Func<T, object>>> { include }.ToImmutableList())
  { }

  public Specification(Expression<Func<T, bool>> criteria, IEnumerable<Expression<Func<T, object>>> includes)
  {
    Criteria = criteria;
    Includes = includes;
  }

  public async ValueTask<IEnumerable<T>> ToListAsync(IQueryable<T> table)
    => await BuildQueryable(table).ToListAsync();

  public async ValueTask<T?> SingleAsync(IQueryable<T> table)
  => await BuildQueryable(table).SingleOrDefaultAsync();

  protected internal Expression<Func<T, bool>> Criteria { get; }

  private Func<T, bool>? compiledCriteria = null;

  public bool Test(in T t)
  {
    compiledCriteria ??= Criteria.Compile();
    return compiledCriteria.Invoke(t);
  }
  protected internal IEnumerable<Expression<Func<T, object>>> Includes { get; }

  public Specification<T> Include(IEnumerable<Expression<Func<T, object>>> includes)
  => includes is null ? this : new Specification<T>(Criteria, Includes.Union(includes));

  public Specification<T> Include(Expression<Func<T, object>> include)
  {
    if (include == null)
    {
      return this;
    }
    List<Expression<Func<T, object>>> includes = new(Includes)
      {
        include
      };
    return Include(includes);
  }

  public IQueryable<T> BuildQueryable(IQueryable<T> q)
  => Includes.Aggregate(seed: q, func: (current, include) => current.Include(include))
    .Where(Criteria);

  public virtual bool Equals([AllowNull] Specification<T> other)
  {
    if (object.ReferenceEquals(this, other))
    {
      return true;
    }
    return other != null && new ExpressionComparison(Criteria, other.Criteria).AreEqual;
  }

  public override bool Equals(object? obj)
  {
    if (object.ReferenceEquals(this, obj))
    {
      return true;
    }
    if (GetType() == obj?.GetType())
    {
      Specification<T> spec = (Specification<T>)obj;
      return new ExpressionComparison(Criteria, spec.Criteria).AreEqual;
    }
    return false;
  }

  public override int GetHashCode()
    => HashCode.Combine(Criteria);

  internal class ExpressionEnumeration : ExpressionVisitor, IEnumerable<Expression>
  {
    private readonly List<Expression> expressions = [];

    public ExpressionEnumeration(Expression expression)
      => Visit(expression);

    public override Expression? Visit(Expression? expression)
    {
      if (expression == null)
      {
        return expression;
      }
      expressions.Add(expression);
      return base.Visit(expression);
    }

    public IEnumerator<Expression> GetEnumerator()
      => expressions.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
      => (this as IEnumerable<Expression>).GetEnumerator();
  }

  internal class ExpressionComparison : ExpressionVisitor
  {
    private readonly Queue<Expression> candidates;
    private Expression? candidate;

    public bool AreEqual { get; private set; } = true;

    public ExpressionComparison(Expression a, Expression b)
    {
      candidates = new Queue<Expression>(new ExpressionEnumeration(b));
      candidate = null;

      _ = Visit(a);

      if (candidates.Count > 0)
      {
        Stop();
      }
    }

    private Expression? PeekCandidate() => candidates.Count == 0 ? null : candidates.Peek();

    private Expression PopCandidate()
      => candidates.Dequeue();

    private bool CheckAreOfSameType(Expression candidate, Expression expression)
      => CheckEqual(expression.NodeType, candidate.NodeType)
      && CheckEqual(expression.Type, candidate.Type);

    private void Stop()
      => AreEqual = false;

    // Change the type of candidate to match original
    private T? MakeCandidateMatch<T>([NotNull] T original) where T : Expression
      => (T?)candidate;

    public override Expression? Visit(Expression? expression)
    {
      if (expression == null || !AreEqual)
      {
        return expression;
      }

      candidate = PeekCandidate();
      if (!CheckNotNull(candidate) || !CheckAreOfSameType(candidate!, expression))
      {
        return expression;
      }

      _ = PopCandidate();

      return base.Visit(expression);
    }

    protected override Expression VisitConstant(ConstantExpression constant)
    {
      ConstantExpression? candidate = MakeCandidateMatch(constant);
      _ = CheckEqual(constant.Value, candidate?.Value);
      return base.VisitConstant(constant);
    }

    protected override Expression VisitMember(MemberExpression member)
    {
      MemberExpression? candidate = MakeCandidateMatch(member);
      _ = CheckEqual(member.Member, candidate?.Member);
      return base.VisitMember(member);
    }

    protected override Expression VisitMethodCall(MethodCallExpression methodCall)
    {
      MethodCallExpression? candidate = MakeCandidateMatch(methodCall);
      _ = CheckEqual(methodCall.Method, candidate?.Method);
      return base.VisitMethodCall(methodCall);
    }

    protected override Expression VisitParameter(ParameterExpression parameter)
    {
      ParameterExpression? candidate = MakeCandidateMatch(parameter);
      _ = CheckEqual(parameter.Name, candidate?.Name);
      return base.VisitParameter(parameter);
    }

    protected override Expression VisitTypeBinary(TypeBinaryExpression type)
    {
      TypeBinaryExpression? candidate = MakeCandidateMatch(type);
      _ = CheckEqual(type.TypeOperand, candidate!.TypeOperand);
      return base.VisitTypeBinary(type);
    }

    protected override Expression VisitBinary(BinaryExpression binary)
    {
      BinaryExpression? candidate = MakeCandidateMatch(binary);
      _ = CheckEqual(binary.Method, candidate!.Method);
      _ = CheckEqual(binary.IsLifted, candidate!.IsLifted);
      _ = CheckEqual(binary.IsLiftedToNull, candidate!.IsLiftedToNull);
      return base.VisitBinary(binary);
    }

    protected override Expression VisitUnary(UnaryExpression unary)
    {
      UnaryExpression? candidate = MakeCandidateMatch(unary);
      _ = CheckEqual(unary.Method, candidate!.Method);
      _ = CheckEqual(unary.IsLifted, candidate.IsLifted);
      _ = CheckEqual(unary.IsLiftedToNull, candidate.IsLiftedToNull);
      return base.VisitUnary(unary);
    }

    protected override Expression VisitNew(NewExpression nex)
    {
      NewExpression? candidate = MakeCandidateMatch(nex);
      _ = CheckEqual(nex.Constructor, candidate!.Constructor);
      CompareList(nex.Members, candidate.Members);
      return base.VisitNew(nex);
    }

    private void CompareList<T>(ReadOnlyCollection<T>? collection, ReadOnlyCollection<T>? candidates)
      => CompareList(collection, candidates, EqualityComparer<T>.Default.Equals);

    private void CompareList<T>(ReadOnlyCollection<T>? collection, ReadOnlyCollection<T>? candidates, Func<T, T, bool> comparer)
    {
      if (!CheckAreOfSameSize(collection, candidates))
      {
        return;
      }

      if (collection is not null && candidates is not null)
      {
        for (int i = 0; i < collection.Count; i++)
        {
          if (!comparer(collection[i], candidates[i]))
          {
            Stop();
            return;
          }
        }
      }
      // ???
    }

    private bool CheckAreOfSameSize<T>(ReadOnlyCollection<T>? collection, ReadOnlyCollection<T>? candidate)
      => CheckEqual(collection?.Count, candidate?.Count);

    private bool CheckNotNull<T>(T? t) where T : class
    {
      if (t == null)
      {
        Stop();
        return false;
      }

      return true;
    }

    private bool CheckEqual<T>(T? t, T? candidate)
    {
      if (!EqualityComparer<T>.Default.Equals(t, candidate))
      {
        Stop();
        return false;
      }
      return true;
    }
  }
}

