namespace U2U.DomainDrivenDesign.Specifications.Tests;

public class SessionTestData : IEntityTestDataConfiguration<Session>
{
  public object[] TestData => [
    new { Id = 1, CourseId = 1, StartDate = new DateTime(2020, 1, 6), EndDate = new DateTime(2020, 1, 10) },
    new { Id = 2, CourseId = 2, StartDate = new DateTime(2020, 1, 6), EndDate = new DateTime(2020, 1, 10) },
    new { Id = 3, CourseId = 3, StartDate = new DateTime(2020, 1, 6), EndDate = new DateTime(2020, 1, 10) },
    new { Id = 4, CourseId = 1, StartDate = new DateTime(2020, 2, 6), EndDate = new DateTime(2020, 2, 10) },
    new { Id = 5, CourseId = 2, StartDate = new DateTime(2020, 3, 6), EndDate = new DateTime(2020, 3, 10) },
    new { Id = 6, CourseId = 3, StartDate = new DateTime(2020, 4, 6), EndDate = new DateTime(2020, 4, 10) },
    new { Id = 7, CourseId = 1, StartDate = new DateTime(2020, 5, 6), EndDate = new DateTime(2020, 5, 10) },
    new { Id = 8, CourseId = 2, StartDate = new DateTime(2020, 6, 6), EndDate = new DateTime(2020, 6, 10) },
    new { Id = 9, CourseId = 3, StartDate = new DateTime(2020, 7, 6), EndDate = new DateTime(2020, 7, 10) }
  ];
}
