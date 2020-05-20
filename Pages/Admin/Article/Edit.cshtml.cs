using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        [BindProperty] public Articles Articles { get; set; }


        [BindProperty] public IFormFile CoverPhoto { get; set; }
        [BindProperty] public IEnumerable<SelectListItem> Tags { get; set; }


        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Articles = await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);
            Tags = await _context.TagCloud.Select(tag => new SelectListItem() {Text = tag.Name, Value = tag.Name})
                .ToListAsync();
            var defaultTags = Articles.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => new SelectListItem() {Text = t, Value = t}).ToList();
            foreach (var item in Tags)
            {
                if (defaultTags.Any(t => t.Value == item.Value))
                {
                    item.Selected = true;
                }
            }

            if (Articles == null)
            {
                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile coverPhoto, IEnumerable<string> tags)
        {
            ModelState.Remove("Articles.CoverPhoto");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Articles).State = EntityState.Modified;

            try
            {
                Articles.CoverPhoto = $"http://placehold.it/750x300?text=null";
                if (coverPhoto != null)
                {
                    Articles.CoverPhoto = $"http://placehold.it/750x300?text={coverPhoto.FileName}";
                }

                var prevTags = string.IsNullOrEmpty(Articles.Tags)?new List<string>():Articles.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                var unchangedTags = prevTags.Intersect(tags);
                var tagsToAdd = tags.Except(unchangedTags);
                var tagsToRemove = prevTags.Except(unchangedTags);
                foreach (var tag in tagsToAdd)
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

                foreach (var tag in tagsToRemove)
                {
                    var tagCloud = _context.TagCloud.SingleOrDefault(t => t.Name == tag);
                    if (tagCloud != null)
                    {
                        if (tagCloud.Amount >= 1)
                        {
                            tagCloud.Amount--;
                            _context.TagCloud.Update(tagCloud);
                        }
                    }
                }

                Articles.Tags = string.Join(",", tags);
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