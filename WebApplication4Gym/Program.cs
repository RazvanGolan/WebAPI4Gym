using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApplication4Gym.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDBContext>(optionsAction:options => 
    options.UseNpgsql("User ID=guest;Password=cocacola;Host=gdscupt.tech;Port=6969;Database=Workshop_golan;Pooling=true;"));

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

//clasa de antrenor, optiunea ca un membru sa aiba antrenor
//data cand s-a inscris cu multe optiuni de introducere