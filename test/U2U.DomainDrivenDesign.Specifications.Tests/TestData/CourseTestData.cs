namespace U2U.DomainDrivenDesign.Specifications.Tests;

public class CourseTestData : IEntityTestDataConfiguration<Course>
{
  public object[] TestData
    => [
        new { Id = 1, Code = "UARCH", Name = "Patterns and Practices" },
        new { Id = 2, Code = "UWEBA", Name = "Advanced Web Development" },
        new { Id = 3, Code = "UCORE", Name = "Upgrade to DotNet Core" },
        new { Id = 4, Code = "UDEF", Name = "Domain Driven Design With Entity Framework Core" }
    ];
}

