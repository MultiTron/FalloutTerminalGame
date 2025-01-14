using FTG.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace FTG.Data;
public class GameDbContext : DbContext
{
    public DbSet<Word> Words { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(new StringBuilder(Environment.GetEnvironmentVariable("CONN_STR")).Append(";Initial Catalog=FalloutTerminalGameDB").ToString());
        base.OnConfiguring(optionsBuilder);
    }
}
