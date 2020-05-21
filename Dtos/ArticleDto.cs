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
            get => _content.Length > 20 ? _content.Substring(0, 20) : _content;
            set => _content = value;
        }

        public string FullContent => _content;

        public DateTime CreatedAt { get; set; }
        public string Tags { get; set; }
    }
}