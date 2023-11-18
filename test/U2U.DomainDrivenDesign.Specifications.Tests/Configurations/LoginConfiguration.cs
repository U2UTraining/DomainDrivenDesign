namespace U2U.DomainDrivenDesign.Specifications.Tests;

public class LoginConfiguration : IEntityTypeConfiguration<Login>
{
  public void Configure(EntityTypeBuilder<Login> builder) 
    => builder
      .HasKey(l => l.Id);
}

