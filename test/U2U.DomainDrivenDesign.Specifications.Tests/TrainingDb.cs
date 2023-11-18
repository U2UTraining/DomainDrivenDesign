using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace U2U.DomainDrivenDesign.Specifications.Tests;
public class TrainingDb : DbContext
{
  public DbSet<Session> Courses => Set<Session>();
  public DbSet<Session> Sessions => Set<Session>();
  public DbSet<Student> Students => Set<Student>();
  public DbSet<Login> Logins => Set<Login>();

  public TrainingDb(DbContextOptions<TrainingDb> options)
    : base(options) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyConfiguration(new CourseConfiguration());
    modelBuilder.ApplyConfiguration(new SessionConfiguration());
    modelBuilder.ApplyConfiguration(new StudentConfiguration());
    modelBuilder.ApplyConfiguration(new StudentSessionConfiguration());
    modelBuilder.ApplyConfiguration(new LoginConfiguration());

    modelBuilder.ApplyTestData(new CourseTestData());
    modelBuilder.ApplyTestData(new SessionTestData());
    modelBuilder.ApplyTestData(new StudentTestData());
    modelBuilder.ApplyTestData(new StudentSessionTestData());
    modelBuilder.ApplyTestData(new LoginTestData());
  }
}

public class TrainingDbContextFactory
  : IDesignTimeDbContextFactory<TrainingDb>
{
  public TrainingDb CreateDbContext(string[] args)
  {
    // This project should use the same user secrets key as Clean.Architecture.Web.csproj !!!
    var cb = new ConfigurationBuilder();
    cb.AddJsonFile("appsettings.json");

    IConfigurationRoot configuration = cb.Build();
    string? connectionString = configuration.GetConnectionString("TrainingDb");

    var builder = new DbContextOptionsBuilder<TrainingDb>();
    builder.UseSqlServer(connectionString);
    builder.EnableSensitiveDataLogging();
    return new TrainingDb(builder.Options);
  }
}
