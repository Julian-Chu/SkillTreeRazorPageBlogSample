using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkillTreeRazorPageBlogSample.Data;
using SkillTreeRazorPageBlogSample.Dtos;
using X.PagedList;

namespace SkillTreeRazorPageBlogSample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly RazorPageBlogContext _context;

        public IPagedList<ArticleDto> Articles { get; set; }

        //for TagCloud partial view
        public IEnumerable<TagDto> Tags { get; set; }

        public IndexModel(ILogger<IndexModel> logger, RazorPageBlogContext context)
        {
            _logger = logger;
            _context = context;
            //for TagCloud partial view
            Tags = _context.TagCloud.Select(t => new TagDto() {Name = t.Name, Amount = t.Amount})
                .OrderByDescending(t => t.Amount);
        }

        public void OnGet(string keyword, int? p)
        {
            var pageIndex = p.HasValue ? p.Value < 1 ? 1 : p.Value : 1;
            var query = _context.Articles.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(a => EF.Functions.Like(a.Body, $"%{keyword}%"));
            }

            Articles = query.OrderBy(a => a.CreateDate).Select(a => new ArticleDto()
            {
                Id = a.Id,
                Content = a.Body,
                Tags = a.Tags,
                Title = a.Title,
                CoverImage = a.CoverPhoto,
                CreatedAt = a.CreateDate
            }).ToPagedList(pageIndex, 5);
            ViewData["keyword"] = keyword;
        }

        public void OnGetTag(string tag, int? p)
        {
            var pageIndex = p.HasValue ? p.Value < 1 ? 1 : p.Value : 1;
            Articles = _context.Articles.Where(a => a.Tags.ToLower().Contains(tag.ToLower())).OrderBy(a => a.CreateDate)
                .Select(a => new ArticleDto()
                {
                    Id = a.Id,
                    Content = a.Body,
                    Tags = a.Tags,
                    Title = a.Title,
                    CoverImage = a.CoverPhoto,
                    CreatedAt = a.CreateDate
                }).ToPagedList(pageIndex, 5);
        }
    }
}