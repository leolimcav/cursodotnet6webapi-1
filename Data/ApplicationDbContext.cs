using ApiCursoDotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCursoDotnet.Data;
public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

  public DbSet<Product>? Products { get; set; }
  public DbSet<Category>? Categories { get; set; }
  public DbSet<Tag>? Tags { get; set; }

  protected override void OnModelCreating(ModelBuilder builder) {
    builder.Entity<Product>().Property(p => p.Name).HasMaxLength(120).IsRequired();
    builder.Entity<Product>().Property(p => p.Code).HasMaxLength(20).IsRequired();
    builder.Entity<Product>().Property(p => p.Description).HasMaxLength(500).IsRequired(false);
    builder.Entity<Category>().ToTable("Categories");
    builder.Entity<Tag>().ToTable("Tags");
  }
}