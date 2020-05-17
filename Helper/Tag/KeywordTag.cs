using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualBasic;

namespace SkillTreeRazorPageBlogSample.Helper.Tag
{
    [HtmlTargetElement("keyword-tag")]
    public class TagsHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _accessor;

        public TagsHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor accessor)
        {
            _urlHelperFactory = urlHelperFactory;
            _accessor = accessor;
        }
        public string Tag { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var actionContext = _accessor.ActionContext;
            var urlHelper = _urlHelperFactory.GetUrlHelper(actionContext);
            var link = urlHelper.PageLink("index");
            output.TagName = "a";
            output.Attributes.Add("href",$"{link.ToLower()}tag/{Tag}");
            output.Attributes.Add("class","btn btn-primary");
            output.Content.SetContent(Tag);
        }
    }
}