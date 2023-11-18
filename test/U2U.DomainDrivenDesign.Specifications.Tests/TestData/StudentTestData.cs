namespace U2U.DomainDrivenDesign.Specifications.Tests;

public class StudentTestData : IEntityTestDataConfiguration<Student>
{
  public object[] TestData => [
    new { Id = 1, FirstName = "Joske", LastName = "Vermeulen" },
    new { Id = 2, FirstName = "Eddy", LastName = "Wally" },
    new { Id = 3, FirstName = "Sam", LastName = "Goris" }
  ];
}

