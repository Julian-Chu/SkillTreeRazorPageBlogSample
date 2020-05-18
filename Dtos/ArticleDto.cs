using System;

namespace SkillTreeRazorPageBlogSample.Dtos
{
    public class ArticleDto
    {
        private string _content;
        public string CoverImage { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Content
        {
            get => _content.Substring(0, 20);
            set => _content = value;
        }

        public DateTime CreatedAt { get; set; }
        public string Tags { get; set; }
    }
}