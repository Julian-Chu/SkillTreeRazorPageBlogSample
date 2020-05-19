using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
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
            Tags = _context.TagCloud.Select(tag => new SelectListItem() {Text = tag.Name, Value = tag.Name}).ToList();
            return Page();
        }

        [BindProperty] public Articles Articles { get; set; }

        [Required] [BindProperty] public IFormFile CoverPhoto { get; set; }
        [BindProperty] public List<SelectListItem> Tags { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile coverPhoto, IEnumerable<string> tags)
        {
            ViewData["test"] = 1;
            ModelState.Remove("Articles.CoverPhoto");
            Articles.Id = Guid.NewGuid();
            Articles.Tags = String.Join(",", tags);
            foreach (var tag in tags)
            {
                var tagCloud = _context.TagCloud.SingleOrDefault(t => t.Name == tag);
                if (tagCloud != null)
                {
                    tagCloud.Amount++;
                    _context.TagCloud.Update(tagCloud);
                }
                else
                {
                    _context.TagCloud.Add(new TagCloud() {Id = Guid.NewGuid(), Amount = 1, Name = tag});
                }
            }

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