namespace U2U.DomainDrivenDesign.Specifications.Tests;

public class Login
{
  public required int Id { get; set; }
  public required string Provider { get; set; }
  public required Student Student { get; set; }
  public required int StudentId { get; set; }
}

