using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkillTreeRazorPageBlogSample.Data;
using SkillTreeRazorPageBlogSample.Dtos;

namespace SkillTreeRazorPageBlogSample.Services
{
    public class ArticleService : IArticleService
    {
        private readonly RazorPageBlogContext _context;

        public void MarkEntityModified(Articles articles)
        {
            _context.Attach(articles).State = EntityState.Modified;
        }

        public ArticleService(RazorPageBlogContext context)
        {
            _context = context;
        }

        public IOrderedQueryable<TagDto> GetTagsByAmountDescending()
        {
            var tags = _context.TagCloud.Select(t => new TagDto() {Name = t.Name, Amount = t.Amount})
                .OrderByDescending(t => t.Amount);
            return tags;
        }

        public IQueryable<Articles> GetArticles()
        {
            return _context.Articles.AsQueryable();
        }

        public DbSet<TagCloud> GetTagsCloud()
        {
            return _context.TagCloud;
        }

        public TagCloud GetTag(string tag)
        {
            return this.GetTagsCloud().SingleOrDefault(t => t.Name == tag);
        }

        public void AddTagToTagCloud(string tag)
        {
            _context.TagCloud.Add(new TagCloud() {Id = Guid.NewGuid(), Amount = 1, Name = tag});
        }

        public void UpdateTagToTagCloud(TagCloud tagCloud)
        {
            _context.TagCloud.Update(tagCloud);
        }

        public void AddArticle(Articles articles)
        {
            _context.Articles.Add(articles);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool IsArticlesExists(Guid id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}