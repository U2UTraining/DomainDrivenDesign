namespace U2U.DomainDrivenDesign.Specifications.Tests;

public class StudentSession
{
  public Session Session { get; set; } = default!;
  public int SessionId { get; set; }
  public Student Student { get; set; } = default!;
  public int StudentId { get; set; }
}
