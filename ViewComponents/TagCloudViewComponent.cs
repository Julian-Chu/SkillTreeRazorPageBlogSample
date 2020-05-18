using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using SkillTreeRazorPageBlogSample.Data;
using SkillTreeRazorPageBlogSample.Dtos;
using SkillTreeRazorPageBlogSample.Pages;

namespace SkillTreeRazorPageBlogSample.ViewComponents
{
    public class TagCloudViewComponent: ViewComponent
    {
        private readonly RazorPageBlogContext _context;
        public IEnumerable<TagDto> Tags { get; set; }
        public TagCloudViewComponent(RazorPageBlogContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            Tags = await _context.TagCloud.Select(t => new TagDto() {Name = t.Name, Amount = t.Amount})
                .OrderByDescending(t => t.Amount).ToListAsync();
            return View(Tags);
        }
    }
}