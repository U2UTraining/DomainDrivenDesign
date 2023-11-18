namespace U2U.DomainDrivenDesign.Specifications.Tests;

public class StudentSessionTestData : IEntityTestDataConfiguration<StudentSession>
{
  public object[] TestData => [
    new { SessionId = 1, StudentId = 1 },
    new { SessionId = 1, StudentId = 3 },
    new { SessionId = 2, StudentId = 2 },
    new { SessionId = 3, StudentId = 1 },
    new { SessionId = 3, StudentId = 2 },
    new { SessionId = 3, StudentId = 3 }
  ];
}
