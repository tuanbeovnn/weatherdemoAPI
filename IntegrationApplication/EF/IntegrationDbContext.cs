using System.Text.Json;
using System.Text.RegularExpressions;
using IntegrationModels;
using Microsoft.EntityFrameworkCore;

namespace IntegrationApplication.EF;

public class IntegrationDbContext : DbContext
{
    public IntegrationDbContext()
    {
    }


    public IntegrationDbContext(DbContextOptions<IntegrationDbContext> options) : base(options)
    {
    }

    public virtual DbSet<WeatherInfoEntity> WeatherInfo { get; set; }
    public virtual DbSet<PostEntity> Posts { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString =
                "Server=code4fun.xyz,1433;Database=web_solutions;User Id=sa;Password=Very_Strong_Password_@@7;";

            optionsBuilder.UseSqlServer(connectionString, options => { options.EnableRetryOnFailure(); });
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties())
            {
                string snakeCaseName = ToSnakeCase(property.Name);
                property.SetColumnName(snakeCaseName);
            }
        }


        modelBuilder.Entity<WeatherInfoEntity>(e =>
        {
            e.HasKey(m => m.Id);
            e.Property(e => e.City).IsRequired();
        });

        modelBuilder.Entity<PostEntity>(e =>
        {
            e.HasKey(m => m.Id);
            e.Property(e => e.Description).IsRequired();
            e.Property(e => e.ShortDescription).IsRequired();
            e.Property(e => e.Title).IsRequired();
            e.Property(e => e.Slug).IsRequired();
            e.Property(e => e.Images)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
                )
                .HasColumnType("NVARCHAR(MAX)");
        });


        base.OnModelCreating(modelBuilder);
    }

    private static string ToSnakeCase(string input)
    {
        return Regex.Replace(input, "([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }
}