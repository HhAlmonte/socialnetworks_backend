using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BussinessLogic.Data
{
    public class ContentDbContext : DbContext
    {
        public ContentDbContext(DbContextOptions<ContentDbContext> options) : base(options)
        {
        }
        public DbSet<PublicationsEntities> PublicationsEntities { get; set; }
        public DbSet<CommentsEntities> CommentsEntities { get; set; }
        public DbSet<AnswersEntities> AnswersEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
