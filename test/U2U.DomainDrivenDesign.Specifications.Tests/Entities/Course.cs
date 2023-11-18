namespace U2U.DomainDrivenDesign.Specifications.Tests;

public class Course
{
  public required int Id { get; set; }
  public required string Code { get; set; }
  public required string Name { get; set; }
  public List<Session> Sessions { get; set; } = new ();
}

