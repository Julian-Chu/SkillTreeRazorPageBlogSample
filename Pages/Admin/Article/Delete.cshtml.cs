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
    public class DeleteModel : PageModel
    {
        private readonly SkillTreeRazorPageBlogSample.Data.RazorPageBlogContext _context;

        public DeleteModel(SkillTreeRazorPageBlogSample.Data.RazorPageBlogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Articles Articles { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Articles = await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);

            if (Articles == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Articles = await _context.Articles.FindAsync(id);

            if (Articles != null)
            {
                _context.Articles.Remove(Articles);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
