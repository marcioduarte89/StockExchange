﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Order.Infrastructure;

public class OrderDbContext : DbContext
{
    private readonly DbContextOptions<OrderDbContext> _contextOptions;
    
    public OrderDbContext(DbContextOptions<OrderDbContext> contextOptions) : base(contextOptions)
    {
        _contextOptions = contextOptions;
    }
    
    public DbSet<Domain.Order> Orders { get; set; }
    
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