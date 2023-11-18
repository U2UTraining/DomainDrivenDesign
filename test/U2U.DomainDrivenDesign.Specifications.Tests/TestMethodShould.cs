using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2U.DomainDrivenDesign.Specifications.Tests;
public class TestMethodShould
{
  private TrainingDb _db;
  public TestMethodShould()
  {
    _db = new TrainingDbContextFactory().CreateDbContext([]);
  }

  [Fact]
  public void AllowCriteriaToBeEvaluated()
  {
    Student student = _db.Students.Find(1)!;
    student.Should().NotBeNull();

    StudentWithId spec1 = new(1);
    spec1.Test(student).Should().BeTrue();

    StudentWithFirstName spec2 = new(student.FirstName);
    spec2.Test(student).Should().BeTrue();

    StudentWithLastName spec3 = new(student.LastName);
    spec3.Test(student).Should().BeTrue();

    Specification<Student> spec4 = spec1.And(spec2).And(spec3);
    spec4.Test(student).Should().BeTrue();

    StudentWithId spec6 = new(6);
    spec6.Test(student).Should().BeFalse();
  }
}

