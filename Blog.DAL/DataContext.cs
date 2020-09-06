using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Blog.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>()
                        .HasKey(u => u.UserId);
            modelBuilder.Entity<User>()
                        .Property(u => u.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<User>()
                        .Property(u => u.Email).IsRequired().HasMaxLength(55);

            modelBuilder.Entity<User>()
                        .Property(u => u.Phone).IsRequired(false).HasMaxLength(55);
            modelBuilder.Entity<User>()
                        .Property(u => u.CreatedAt).HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()"); ;

            modelBuilder.Entity<User>()
                        .HasMany(c => c.Blogs)
                        .WithOne(e => e.User);



            modelBuilder.Entity<Blog.Models.Blog>()
                        .HasKey(b => b.BlogId);
            modelBuilder.Entity<Blog.Models.Blog>()
                        .Property(b => b.Content).IsRequired().HasMaxLength(maxLength: 5000);

            modelBuilder.Entity<Blog.Models.Blog>()
                      .Property(b => b.CreatedAt).HasColumnType("datetime")
                      .HasDefaultValueSql("getdate()"); ;

            modelBuilder.Entity<Blog.Models.Blog>()
            .Property(b => b.Title).IsRequired().HasMaxLength(maxLength: 75);


            modelBuilder.Entity<Blog.Models.Blog>()
                         .HasMany(c => c.Comments)
                         .WithOne(e => e.Blog);


            modelBuilder.Entity<Comment>()
                         .HasKey(c => c.CommentId);
            modelBuilder.Entity<Comment>()
                        .Property(c => c.Content).IsRequired().HasMaxLength(500);
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Blog.Models.Blog> Blogs { get; set; }
    }
}
