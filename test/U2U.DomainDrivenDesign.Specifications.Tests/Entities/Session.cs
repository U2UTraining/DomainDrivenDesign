namespace U2U.DomainDrivenDesign.Specifications.Tests;

public class Session
{
  public required int Id { get; set; }
  public required DateTime StartDate { get; set; }
  public required DateTime EndDate { get; set; }
  public required Course Course { get; set; }
  public required int CourseId { get; set; }
  public List<StudentSession> Students { get; set; } = new();
}

