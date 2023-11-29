using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using WebApplication4Gym.Entities;
using WebApplication4Gym.Entities.Coach;
using WebApplication4Gym.Entities.Member;

namespace WebApplication4Gym.Repository;

public class AppDBContext : DbContext
{
    
    public AppDBContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<Member> Members { get; set; }
    public DbSet<Coach> Coaches { get; set; }
    
}