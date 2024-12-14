using FastEndpoints;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Order.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFastEndpoints();

builder.Services.AddHangfire((provider, config) =>
    config.UsePostgreSqlStorage(c =>
        c.UseNpgsqlConnection(provider.GetService<IConfiguration>()!.GetConnectionString("Order"))));

builder.Services.AddHangfireServer(options =>
{
    options.ServerName = "OrderAPIBackgroundServer";
});

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Order")));

builder.Services.AddScoped<IOrderDbContext, OrderDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseFastEndpoints();
app.UseHangfireDashboard();

GlobalConfiguration.Configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(x => x.UseNpgsqlConnection(app.Configuration.GetConnectionString("Order")));

app.Run();