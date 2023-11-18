namespace U2U.DomainDrivenDesign.Specifications.Tests;

internal class StudentWithLastName(string firstName) 
: Specification<Student>(student => student.LastName == firstName)
{}
