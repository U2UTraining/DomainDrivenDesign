namespace U2U.DomainDrivenDesign.Specifications.Tests;

public class LoginTestData : IEntityTestDataConfiguration<Login>
{
  public object[] TestData => [
    new { Id = 1, Provider = "X", StudentId = 1 },
    new { Id = 2, Provider = "X", StudentId = 2 },
    new { Id = 3, Provider = "X", StudentId = 3 }
  ];
}
