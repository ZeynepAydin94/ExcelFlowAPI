using ExcelFlow.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExcelFlow.DataAccess.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User>? Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User tablosu için örnek bir yapılandırma
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.RecordId);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
        });
    }
}


