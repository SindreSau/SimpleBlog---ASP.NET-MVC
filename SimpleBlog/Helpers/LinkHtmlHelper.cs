using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleBlog.Models;

namespace SimpleBlog.Helpers
{
    public static class LinkHtmlHelper
    {
        public static async Task<IHtmlContent> LinkAsync(this IHtmlHelper htmlHelper, string href, string text, LinkType type = LinkType.Default)
        {
            var model = new LinkViewModel
            {
                Href = href,
                Text = text,
                Type = type
            };

            return await htmlHelper.PartialAsync("_Link", model);
        }
    }
}