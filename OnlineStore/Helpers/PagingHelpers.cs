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
                TagBuilder li = new TagBuilder("li");

                TagBuilder tag = new TagBuilder("a");

                li.AddCssClass("page-item");

                tag.MergeAttribute("href", pageUrl(i));

                tag.InnerHtml = i.ToString();

                if (i == pageInfo.PageNumber)
                {
                    li.AddCssClass("active");
                }

                tag.AddCssClass("page-link");

                li.InnerHtml = tag.ToString();

                result.Append(li.ToString());  
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}
