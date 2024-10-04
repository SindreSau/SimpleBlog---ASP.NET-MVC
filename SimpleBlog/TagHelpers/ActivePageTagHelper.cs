using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SimpleBlog.TagHelpers
{
    [HtmlTargetElement(Attributes = "is-active-route")]
    public class ActivePageTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-controller")]
        public string? Controller { get; set; }

        [HtmlAttributeName("asp-action")]
        public string? Action { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext? ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var currentController = ViewContext.RouteData.Values["Controller"] as string;
            var currentAction = ViewContext.RouteData.Values["Action"] as string;

            if (string.Equals(Controller, currentController, StringComparison.OrdinalIgnoreCase) &&
                (string.IsNullOrEmpty(Action) || string.Equals(Action, currentAction, StringComparison.OrdinalIgnoreCase)))
            {
                if (output.Attributes.ContainsName("class"))
                {
                    output.Attributes.SetAttribute("class", $"{output.Attributes["class"].Value} active");
                }
                else
                {
                    output.Attributes.SetAttribute("class", "active");
                }
            }
        }
    }
}