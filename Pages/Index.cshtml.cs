using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SkillTreeRazorPageBlogSample.Data;

namespace SkillTreeRazorPageBlogSample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly RazorPageBlogContext _context;
        public IList<ArticleDto> Articles { get; set; }

        public IndexModel(ILogger<IndexModel> logger, RazorPageBlogContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            Articles = _context.Articles.Select(a => new ArticleDto()
            {
                Id = a.Id,
                Content = a.Body,
                Tags = a.Tags,
                Title = a.Title,
                CoverImage = a.CoverPhoto,
                CreatedAt = a.CreateDate
            }).ToList();
        }

        public void OnGetKeywordTag(string tag)
        {
            Articles = _context.Articles.Where(a=>a.Tags.Contains(tag)).Select(a => new ArticleDto()
            {
                Id = a.Id,
                Content = a.Body,
                Tags = a.Tags,
                Title = a.Title,
                CoverImage = a.CoverPhoto,
                CreatedAt = a.CreateDate
            }).ToList();
        }
    }

    public class ArticleDto
    {
        private string _content;
        public string CoverImage { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Content
        {
            get => _content.Substring(0,20);
            set => _content = value;
        }

        public DateTime CreatedAt { get; set; }
        public string Tags { get; set; }
    }
}
