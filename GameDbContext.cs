using FTG.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FTG.Data;
public class GameDbContext : DbContext
{
    public DbSet<Word> Words { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("CONN_STR"));
        base.OnConfiguring(optionsBuilder);
    }
}
