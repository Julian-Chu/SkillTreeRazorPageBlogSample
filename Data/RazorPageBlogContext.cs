using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SkillTreeRazorPageBlogSample.Data
{
    public partial class RazorPageBlogContext:DbContext
    {
        public RazorPageBlogContext(DbContextOptions<RazorPageBlogContext> options):base(options)
        {
            
        }

        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<TagCloud> TagCloud { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articles>(e => e.Property(x => x.Id).ValueGeneratedNever());
            modelBuilder.Entity<TagCloud>(e => e.Property(x => x.Id).ValueGeneratedNever());
        }
    }
}