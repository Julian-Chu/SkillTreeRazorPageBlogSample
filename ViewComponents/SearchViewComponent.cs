using Microsoft.AspNetCore.Mvc;

namespace SkillTreeRazorPageBlogSample.ViewComponents
{
    public class SearchViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(string keyword)
        {
            ViewData["keyword"] = keyword;
            return View();
        }
    }

}