using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using OnlineStore.Models.ViewModels;

namespace OnlineStore.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {

                TagBuilder tag = new TagBuilder("a");

                tag.AddCssClass("product__pagination__number");

                tag.MergeAttribute("href", pageUrl(i));

                tag.InnerHtml = i.ToString();

                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("pagination__active");
                }

                result.Append(tag.ToString());
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}
