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
using SkillTreeRazorPageBlogSample.Services;

namespace SkillTreeRazorPageBlogSample.Pages.Admin.Article
{
    public class ArticleFormModel : PageModel
    {
        [BindProperty] public Articles Articles { get; set; }
        [BindProperty] public virtual IFormFile CoverPhoto { get; set; }
        [BindProperty] public List<SelectListItem> Tags { get; set; }
    }

    public class CreateModel : ArticleFormModel
    {
        [Required]
        [BindProperty]
        public override IFormFile CoverPhoto { get; set; }
        private readonly IArticleService _service;

        public CreateModel(IArticleService service)
        {
            _service = service;
        }

        public IActionResult OnGet()
        {
            Tags = _service.GetTagsCloud().Select(tag => new SelectListItem() {Text = tag.Name, Value = tag.Name})
                .ToList();
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile coverPhoto, IEnumerable<string> tags)
        {
            ViewData["test"] = 1;
            ModelState.Remove("Articles.CoverPhoto");
            ModelState.Remove("Articles.Id");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Articles.Id = Guid.NewGuid();
            Articles.Tags = String.Join(",", tags);
            foreach (var tag in tags)
            {
                var tagCloud = _service.GetTag(tag);
                if (tagCloud != null)
                {
                    tagCloud.Amount++;
                    _service.UpdateTagToTagCloud(tagCloud);
                }
                else
                {
                    _service.AddTagToTagCloud(tag);
                }
            }

            Articles.CoverPhoto = $"http://placehold.it/750x300?text={coverPhoto.Name}";

            _service.AddArticle(Articles);
            await _service.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}