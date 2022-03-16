using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.EFCore
{
    public class CommentContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public CommentContext(DbContextOptions<CommentContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CommentMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
