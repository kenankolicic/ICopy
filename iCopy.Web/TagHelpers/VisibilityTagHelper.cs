using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace iCopy.Web.TagHelpers
{
    [HtmlTargetElement("*", Attributes = VisibleAttributeName)]
    public class VisibilityTagHelper : TagHelper
    {
        private const string VisibleAttributeName = "is-visible";

        [HtmlAttributeName(VisibleAttributeName)]
        public bool Visible { get; set; } = true;

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!Visible)
                output.SuppressOutput();

            return base.ProcessAsync(context, output);
        }
    }
}
