using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace User.Infrastructure.EF;

public class UserDbContext : IdentityDbContext<Domain.User, IdentityRole<long>, long>
{
    private readonly DbContextOptions<UserDbContext> _contextOptions;
    
    public UserDbContext(DbContextOptions<UserDbContext> contextOptions) : base(contextOptions)
    {
        _contextOptions = contextOptions;
    }
    
    public DbSet<Domain.User> Users { get; set; }
    
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.
    //         UseNpgsql(_contextOptions.)
    //         .UseSnakeCaseNamingConvention();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}