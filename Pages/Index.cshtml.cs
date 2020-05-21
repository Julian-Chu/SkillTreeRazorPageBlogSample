using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkillTreeRazorPageBlogSample.Dtos;
using SkillTreeRazorPageBlogSample.Services;
using X.PagedList;

namespace SkillTreeRazorPageBlogSample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IArticleService _service;

        public IPagedList<ArticleDto> Articles { get; set; }

        //for TagCloud partial view
        public IEnumerable<TagDto> Tags { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IArticleService service)
        {
            _logger = logger;
            _service = service;
            //for TagCloud partial view
            Tags = _service.GetTagsByAmountDescending();
        }


        public void OnGet(string keyword, int? p)
        {
            var pageIndex = p.HasValue ? p.Value < 1 ? 1 : p.Value : 1;
            var query = _service.GetArticles();
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
            Articles = _service.GetArticles().Where(a => a.Tags.ToLower().Contains(tag.ToLower()))
                .OrderBy(a => a.CreateDate)
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