using Microsoft.EntityFrameworkCore;
using Project.Data.Models;

namespace Project.Data;

public class TaskDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<TaskEntity> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<TaskEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Task)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Details)
                .HasMaxLength(500);
            entity.Property(e => e.AssignedToName)
                .HasMaxLength(100);
            entity.Property(e => e.AssignedToEmail)
                .IsRequired()
                .HasMaxLength(100);
        });

        base.OnModelCreating(mb);
    }
}