using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApplication4Gym.Repository;
using WebApplication4Gym.Entities.Member;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;// get connection string from appsettings.Development.json
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDBContext>(optionsAction:options => 
    options.UseNpgsql(configuration.GetConnectionString("ConnectionString")));
builder.Services.AddScoped<IMemberRepository, MemberRepository>();

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); //for DateTime error when reading

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

