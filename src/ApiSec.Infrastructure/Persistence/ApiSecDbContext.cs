using ApiSec.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ApiSec.Infrastructure.Persistence;
public class ApiSecDbContext : DbContext
{
    public ApiSecDbContext()
    {
        
    }
    public ApiSecDbContext(DbContextOptions<ApiSecDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<User>()
            .Property(_ => _.Email)
            .HasMaxLength(250)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(_ => _.Password)
            .HasMaxLength(250)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(_ => _.Active)
            .HasColumnType("bit")
            .IsRequired();

        modelBuilder.Entity<UserRole>()
            .Property(_ => _.Role)
            .HasMaxLength(50)
            .IsRequired();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        var conn = "SERVER=localhost;DATABASE=DB_Autentication;USER ID=sa;PASSWORD=SqlPassword123@";
        if (!optionsBuilder.IsConfigured)
        {
                optionsBuilder.UseSqlServer(conn);
        }
    }
}
