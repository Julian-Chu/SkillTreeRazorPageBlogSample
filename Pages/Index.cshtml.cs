using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SkillTreeRazorPageBlogSample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<ArticleDto> Articles = new List<ArticleDto>();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
         Articles.Add(new ArticleDto()
         {
             Id =  Guid.NewGuid(),
             Title = "Test Title",
             Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis aliquid atque, nulla? Quos cum ex quis soluta, a laboriosam. Dicta expedita corporis animi vero voluptate voluptatibus possimus, veniam magni quis!",
             CoverImage = "http://placehold.it/750x300",
             Tags = "cloud,azure,dotnetcore",
             CreatedAt = DateTime.Now
         }); 
        }
    }

    public class ArticleDto
    {
        public string CoverImage { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string  Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Tags { get; set; }
    }
}
