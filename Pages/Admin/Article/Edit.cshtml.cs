using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkillTreeRazorPageBlogSample.Data;

namespace SkillTreeRazorPageBlogSample.Pages.Admin.Article
{
    public class EditModel : PageModel
    {
        private readonly SkillTreeRazorPageBlogSample.Data.RazorPageBlogContext _context;

        public EditModel(SkillTreeRazorPageBlogSample.Data.RazorPageBlogContext context)
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Articles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticlesExists(Articles.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ArticlesExists(Guid id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
