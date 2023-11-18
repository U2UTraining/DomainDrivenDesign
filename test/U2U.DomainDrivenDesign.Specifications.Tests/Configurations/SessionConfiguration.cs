namespace U2U.DomainDrivenDesign.Specifications.Tests;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
  public void Configure(EntityTypeBuilder<Session> builder)
  {
    builder
     .HasKey(s => s.Id);
    builder
      .HasOne(s => s.Course)
      .WithMany(c => c.Sessions)
      .HasForeignKey(s => s.CourseId);
  }
}

