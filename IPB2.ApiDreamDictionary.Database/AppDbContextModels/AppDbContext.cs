using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IPB2.ApiDreamDictionary.Database.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BlogHeader> BlogHeaders { get; set; }

    public virtual DbSet<Blogdetail> Blogdetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=IPB2DD;User ID=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogHeader>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("BlogHeader");

            entity.Property(e => e.BlogId).ValueGeneratedNever();
            entity.Property(e => e.BlogTitle).HasMaxLength(200);
        });

        modelBuilder.Entity<Blogdetail>(entity =>
        {
            entity.ToTable("Blogdetail");

            entity.Property(e => e.BlogDetailId).ValueGeneratedNever();
            entity.Property(e => e.BlogContent).HasMaxLength(1000);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
