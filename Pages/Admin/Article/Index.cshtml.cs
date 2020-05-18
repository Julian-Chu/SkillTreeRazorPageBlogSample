using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkillTreeRazorPageBlogSample.Data;

namespace SkillTreeRazorPageBlogSample.Pages.Admin.Article
{
    public class IndexModel : PageModel
    {
        private readonly SkillTreeRazorPageBlogSample.Data.RazorPageBlogContext _context;

        public IndexModel(SkillTreeRazorPageBlogSample.Data.RazorPageBlogContext context)
        {
            _context = context;
        }

        public IList<Articles> Articles { get;set; }

        public async Task OnGetAsync()
        {
            Articles = await _context.Articles.ToListAsync();
        }
    }
}
