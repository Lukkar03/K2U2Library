using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace K2U2Library;

public partial class K2u2library2DbContext : DbContext
{
    public K2u2library2DbContext()
    {
    }

    public K2u2library2DbContext(DbContextOptions<K2u2library2DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=K2U2Library2DB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Book__3DE0C207BD9706C1");

            entity.ToTable("Book");

            entity.HasIndex(e => e.Author, "IX_Book_Author");

            entity.HasIndex(e => e.Title, "IX_Book_Title");

            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.Genre).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("PK__Loan__4F5AD4572B29ED5E");

            entity.ToTable("Loan", tb => tb.HasTrigger("TR_Loan_PreventDoubleLoan"));

            entity.HasIndex(e => e.FkbookId, "IX_Loan_FKBookId");

            entity.HasIndex(e => e.FkmemberId, "IX_Loan_FKMemberId");

            entity.HasIndex(e => e.ReturnDate, "IX_Loan_ReturnDate");

            entity.HasIndex(e => e.Status, "IX_Loan_Status");

            entity.Property(e => e.FkbookId).HasColumnName("FKBookId");
            entity.Property(e => e.FkmemberId).HasColumnName("FKMemberId");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Fkbook).WithMany(p => p.Loans)
                .HasForeignKey(d => d.FkbookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Loan_Book");

            entity.HasOne(d => d.Fkmember).WithMany(p => p.Loans)
                .HasForeignKey(d => d.FkmemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Loan_Member");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Member__0CF04B1892CD31C7");

            entity.ToTable("Member");

            entity.HasIndex(e => e.Email, "IX_Member_Email").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Member__A9D105341AA3DDCA").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
