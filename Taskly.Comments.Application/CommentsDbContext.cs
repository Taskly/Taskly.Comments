using Microsoft.EntityFrameworkCore;
using Taskly.Comments.Application.Entities;

namespace Taskly.Comments.Application
{
    public class CommentsDbContext : DbContext
    {
        public CommentsDbContext(DbContextOptions<CommentsDbContext> options)
            : base(options)
        {
        }

        public DbSet<CommentEntity> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentEntity>()
                .ToTable("comments");

            /*modelBuilder.Entity<Comment>()
                .HasDiscriminator(x => x.Status)
                .HasValue(CommentStatus.Active);
            modelBuilder.Entity<Comment>()
                .ToTable("comments")
                .OwnsOne(x => x.Locator);

            modelBuilder.Entity<DeletedComment>()
                .HasDiscriminator(x => x.Status)
                .HasValue(CommentStatus.Deleted);
            modelBuilder.Entity<DeletedComment>()
                .Property<string>("Locator_Section");
            modelBuilder.Entity<DeletedComment>()
                .Property<string>("Locator_Subsection");
            modelBuilder.Entity<DeletedComment>()
                .Property<string>("Locator_Element");*/
        }
    }
}
