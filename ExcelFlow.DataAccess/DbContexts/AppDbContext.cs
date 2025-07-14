using ExcelFlow.Core.Entities;
using ExcelFlow.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExcelFlow.DataAccess.DbContexts;

public class AppDbContext : DbContext
{
    private readonly ICurrentUserService _currentUserService;

    public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUserService currentUserService) : base(options)
    {
        _currentUserService = currentUserService;
    }
    public DbSet<User>? Users { get; set; }
    public DbSet<UploadJob>? UploadJob { get; set; }
    public DbSet<UploadStatus>? UploadStatus { get; set; }
    public DbSet<ExcelTemplate>? ExcelTemplates { get; set; }
    public DbSet<ExcelTemplateColumn>? ExcelTemplateColumns { get; set; }
    public DbSet<ExcelTemplateColumnValidation> ExcelTemplateColumnValidations { get; set; }
    public DbSet<UploadJobError> UploadJobErrors { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User tablosu için örnek bir yapılandırma
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.RecordId);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
        });
        modelBuilder.Entity<UploadJob>(entity =>
      {
          entity.HasKey(e => e.RecordId);
      });
        modelBuilder.Entity<UploadStatus>(entity =>
       {
           entity.HasKey(e => e.RecordId);
       });
        modelBuilder.Entity<ExcelTemplate>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.TargetTable).IsRequired().HasMaxLength(200);
        });
        modelBuilder.Entity<ExcelTemplateColumn>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ExcelColumnName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.TargetColumnName).IsRequired().HasMaxLength(100);
        });
        modelBuilder.Entity<UploadJobError>(entity =>
        {
            entity.HasKey(e => e.RecordId);
        });
        // Diğer entity yapılandırmaları...
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        ApplyAuditInformation(); // Her SaveChanges çağrısında audit bilgileri doldurulur
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    private void ApplyAuditInformation()
    {
        // Değişiklik izleyicisindeki Added veya Modified durumundaki BaseEntity türündeki girişleri bul
        var entries = ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        int? currentUserId = _currentUserService.GetUserId(); // Mevcut kullanıcı ID'sini al
        DateTime now = DateTime.UtcNow; // UTC saat kullanmak her zaman iyi bir uygulamadır

        foreach (var entry in entries)
        {
            // Eğer entity yeni ekleniyorsa
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedByUserId = currentUserId;
                entry.Entity.CreatedAt = now;

            }
            // // Eğer entity güncelleniyorsa
            // else if (entry.State == EntityState.Modified)
            // {
            //     //entry.Entity. = currentUserId;
            //     entry.Entity.UpdatedAt = now;

            //     // Önemli: Oluşturma bilgilerinin manuel olarak güncellenmesini engelle
            //     // Bu, CreatedAt ve CreatedByUserId'nin sadece ilk oluşturmada ayarlanmasını sağlar.
            //     entry.Property(nameof(BaseEntity.CreatedAt)).IsModified = false;
            //     entry.Property(nameof(BaseEntity.CreatedByUserId)).IsModified = false;
            // }
        }
    }


}