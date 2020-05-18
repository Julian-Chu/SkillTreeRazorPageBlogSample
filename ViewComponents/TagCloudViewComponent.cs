using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using SkillTreeRazorPageBlogSample.Data;

namespace SkillTreeRazorPageBlogSample.ViewComponents
{
    public class TagCloudViewComponent: ViewComponent
    {
        private readonly RazorPageBlogContext _context;

        public TagCloudViewComponent(RazorPageBlogContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var tags = await _context.TagCloud.ToListAsync();
            return View(tags);
        }
    }
}