using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkillTreeRazorPageBlogSample.Data;
using SkillTreeRazorPageBlogSample.Dtos;

namespace SkillTreeRazorPageBlogSample.Services
{
    public interface IArticleService
    {
        void MarkEntityModified(Articles articles);
        IOrderedQueryable<TagDto> GetTagsByAmountDescending();
        IQueryable<Articles> GetArticles();
        DbSet<TagCloud> GetTagsCloud();
        TagCloud GetTag(string tag);
        void AddTagToTagCloud(string tag);
        void UpdateTagToTagCloud(TagCloud tagCloud);
        void AddArticle(Articles articles);
        Task SaveChangesAsync();
        bool IsArticlesExists(Guid id);
    }
}