using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SkillTreeRazorPageBlogSample.Data;

namespace Blog_RazorPage_SkillTree.Model
{
    public static class SeedData
    {
         public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RazorPageBlogContext(
                                                          serviceProvider.GetRequiredService<
                                                              DbContextOptions<RazorPageBlogContext>>()))
            {
                if (context.Articles.Any())
                {
                    return;
                }
                var tmp = new List<Articles>();
                var tags = new StringBuilder();
                for (int i = 1; i <= 20; i++)
                {
                    var tag = RandomTag();
                    tmp.Add(new Articles
                    {
                        Id = Guid.NewGuid(),
                        Title = $"第{i}筆部落格",
                        Body = LoremIpsum(),
                        CoverPhoto = $"http://placehold.it/750x300?text=This is {i}",
                        CreateDate = DateTime.UtcNow.AddDays(i),
                        Tags = tag,
                    });
                    tags.Append(tag + ",");
                }

                var tagCloud = tags.ToString()
                    .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                    .GroupBy(d => d)
                    .Select(d => new { Key = d.Key, Amount = d.Count() })
                    .ToList();
                foreach (var item in tagCloud)
                {
                    context.TagCloud.Add(new TagCloud
                    {
                        Id = Guid.NewGuid(),
                        Name = item.Key,
                        Amount = item.Amount
                    });
                }
                context.Articles.AddRange(tmp);
                context.SaveChanges();
            }
        }

        private static string RandomTag()
        {
            var tags = new List<string>() { "SkillTree", "twMVC", "demoshop", "Dotblogs", "RazorPage" };
            //先決定要取幾個
            var take = Enumerable.Range(1, 5).OrderBy(d => Guid.NewGuid()).First();
            //再亂數取幾個
            return string.Join(",", tags.OrderBy(d => Guid.NewGuid()).Take(take));

        }

        private static string LoremIpsum()
        {

            using (var webClient = new WebClient())
            {
                var baseUri = "http://more.handlino.com/sentences.json?n=8";
                webClient.Encoding = Encoding.UTF8;
                var jsonString = webClient.DownloadString(new Uri(baseUri));
                var oJson      =  JsonSerializer.Deserialize<LoremIpsum>(jsonString);
                return"<p>" + String.Join("</p><p>",  oJson.sentences) + "</p>";

            }
        }
    }
}
