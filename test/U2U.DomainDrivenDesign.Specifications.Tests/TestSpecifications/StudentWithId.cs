namespace U2U.DomainDrivenDesign.Specifications.Tests;

internal class StudentWithId(int id) 
: Specification<Student>(Student=>Student.Id == id)
{}
