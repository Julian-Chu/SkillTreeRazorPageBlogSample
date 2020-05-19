using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkillTreeRazorPageBlogSample.Data;

namespace SkillTreeRazorPageBlogSample.Pages.Admin.Article
{
    public class CreateModel : PageModel
    {
        private readonly SkillTreeRazorPageBlogSample.Data.RazorPageBlogContext _context;

        public CreateModel(SkillTreeRazorPageBlogSample.Data.RazorPageBlogContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty] 
        public Articles Articles { get; set; }

        [Required] 
        [BindProperty] 
        public IFormFile CoverPhoto { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile coverPhoto)
        {
            ModelState.Remove("Articles.CoverPhoto");
            Articles.Id = Guid.NewGuid();
            
            Articles.CoverPhoto = $"http://placehold.it/750x300?text={coverPhoto.Name}";
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Articles.Add(Articles);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}