using System.Reflection;
using Authenticate;
using Authenticate.Context;
using Authenticate.Interfaces;
using Authenticate.Mappings;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterMapsterConfiguration();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<IJWTProvider, JWTProvider>();


string? defName = builder.Configuration["Db:Name"];
string? defHost = builder.Configuration["Db:Host"];
string? defPass = builder.Configuration["Db:Pass"];
string? dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? defHost;
string? dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? defName;
string? dbPass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD") ?? defPass;
string connectionString = $"Server={dbHost}; Persist Security Info=False; TrustServerCertificate=true; User ID=sa;Password={dbPass};Initial Catalog={dbName};";
builder.Services.AddDbContext<AuthenticateContextDb>(options => options.UseSqlServer(connectionString));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
