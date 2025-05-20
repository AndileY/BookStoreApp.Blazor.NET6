using System;
using System.Collections.Generic;
//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookStoreAppApI.Data
{
    public partial class BookStoreAppDboContext : IdentityDbContext<ApiUser>
    {
        public BookStoreAppDboContext()
        {
        }

        public BookStoreAppDboContext(DbContextOptions<BookStoreAppDboContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-NCFE678; Database=BookStoreAppDbo; Trusted_Connection=True; MultipleActiveResultSets=true");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Bio)
                    .HasMaxLength(250)
                    .IsFixedLength();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EA1F39AA15")
                    .IsUnique();

                entity.Property(e => e.Image)
                    .HasMaxLength(250)
                    .IsFixedLength();

                entity.Property(e => e.Isbn)
                    .HasMaxLength(50)
                    .HasColumnName("ISBN")
                    .IsFixedLength();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Summary)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Books_ToTable");
            });

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = "a123e426-b5e6-4664-a7a7-5564c344f15e"
                },
                 new IdentityRole
                 {
                     Name = "Administrator",
                     NormalizedName = "ADMINISTRATOR",
                     Id = "0284a2b8-98ee-4608-9dd2-1376b009888e",
                 }
              );
            var hasher = new PasswordHasher<ApiUser>();
            modelBuilder.Entity<ApiUser>().HasData(
                new ApiUser
                {
                    Id = "2c6bc221-9937-49ce-99ed-293051f4f106",
                    Email = "admin@bookstore.com",
                    NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                    UserName = "admin@bookstore.com",
                    NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                    FirstName = "System",
                    LastName = "Admin",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1")
                },
                 new ApiUser
                 {
                     Id = "ee9f8614-dc40-463b-a450-be6bc5fd6914",
                     Email = "user@bookstore.com",
                     NormalizedEmail = "USER@BOOKSTORE.COM",
                     UserName = "user@bookstore.com",
                     NormalizedUserName = "USER@BOOKSTORE.COM",
                     FirstName = "System",
                     LastName = "User",

                     PasswordHash = hasher.HashPassword(null, "P@ssword1")

                 }
                ); ;
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "a123e426-b5e6-4664-a7a7-5564c344f15e",
                    UserId = "ee9f8614-dc40-463b-a450-be6bc5fd6914"

                },
                new IdentityUserRole<string>
                {
                    RoleId = "0284a2b8-98ee-4608-9dd2-1376b009888e",
                    UserId = "2c6bc221-9937-49ce-99ed-293051f4f106"

                }

                );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
