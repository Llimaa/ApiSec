using System.Text;
using ApiSec.Api.Configuration;
using ApiSec.Application.Repositories;
using ApiSec.Application.Services;
using ApiSec.Infrastructure.Auth;
using ApiSec.Infrastructure.Persistence.Context;
using ApiSec.Infrastructure.Persistence.Repositories;
using ApiSec.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ApiSec.Application.AggregatesModel.CreateUserAggregates;
using ApiSec.Application.AggregatesModel.CreateUserRoleAggregates;
using ApiSec.Application.Queries.FindUser;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "APISEC.API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header usando o esquema Bearer."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type =ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? ""))
        };
    });

builder.Services.AddHealthChecks();
builder.Services.AddTransient(typeof(IDBConfiguration), typeof(MSSQLConfiguration));
builder.Services.AddTransient(typeof(IDB), typeof(MSSQLContext));

//  builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseInMemoryDatabase(Configuration.GetConnectionString("DevFreelaCs")));
builder.Services.AddDbContext<ApiSecDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();

builder.Services.AddScoped<ICreateUser, CreateUser>();
builder.Services.AddScoped<ICreateUserRole, CreateUserRole>();
builder.Services.AddScoped<ILoginUser, LoginUser>();
builder.Services.AddScoped<IFindUser, FindUser>();

var app = builder.Build();

app.MapHealthChecks("/healthz");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiSec.API v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication(); // sempre vem primeiro que o UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
