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

        public DbSet<DeletedCommentEntity> DeletedComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentEntity>().ToTable("comments");
            modelBuilder.Entity<DeletedCommentEntity>().ToTable("deleted_comments");
        }
    }
}
