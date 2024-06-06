using Duende.IdentityServer;
using IdentityServer.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
const string connectionString = @"Data Source=Duende.IdentityServer.Quickstart.EntityFramework.db";

builder.Services.AddIdentityServer()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b => b.UseNpgsql(builder.Configuration.GetConnectionString("User"),
            sql => sql.MigrationsAssembly(migrationsAssembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b => b.UseNpgsql(builder.Configuration.GetConnectionString("User"),
            sql => sql.MigrationsAssembly(migrationsAssembly));
    })
    .AddTestUsers(TestUsers.Users);


// builder.Services.AddIdentityServer()
//     .AddInMemoryIdentityResources(Config.IdentityResources)
//     .AddInMemoryApiScopes(Config.ApiScopes)
//     .AddInMemoryClients(Config.Clients);
//     // .AddTestUsers(TestUsers.Users);

var authenticationBuilder = builder.Services.AddAuthentication();

authenticationBuilder.AddOpenIdConnect("oidc", "IdentityServer", options =>
{
    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
    options.SignOutScheme = IdentityServerConstants.SignoutScheme;
    options.SaveTokens = true;

    options.Authority = "https://demo.duendesoftware.com";
    options.ClientId = "interactive.confidential";
    options.ClientSecret = "secret";
    options.ResponseType = "code";

    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = "name",
        RoleClaimType = "role"
    };
});


var app = builder.Build();

Initialize.InitializeDatabase(app);
app.UseIdentityServer();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
