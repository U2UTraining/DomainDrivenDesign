namespace U2U.DomainDrivenDesign.Specifications.Tests;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
  public void Configure(EntityTypeBuilder<Student> builder)
  {
    builder
      .HasKey(st => st.Id);

    builder
      .HasOne(s => s.Login)
      .WithOne(l => l.Student)
      .HasForeignKey<Login>(l => l.StudentId);
  }
}

