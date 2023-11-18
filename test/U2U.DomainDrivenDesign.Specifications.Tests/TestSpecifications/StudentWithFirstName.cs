namespace U2U.DomainDrivenDesign.Specifications.Tests;

internal class StudentWithFirstName(string firstName) 
: Specification<Student>(student => student.FirstName == firstName)
{}
