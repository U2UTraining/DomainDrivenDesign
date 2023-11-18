namespace U2U.DomainDrivenDesign.Specifications.Tests;

public class Student
{
  public required int Id { get; set; }
  public required string FirstName { get; set; }
  public required string LastName { get; set; }
  public required Login Login { get; set; }
  public List<StudentSession> Sessions { get; set; } = new();
}
