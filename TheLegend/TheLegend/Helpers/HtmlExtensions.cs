using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TheLegend.Models;

namespace TheLegend.Helpers
{
    public static class HtmlExtensions
    {
        public static string TagCloud(this HtmlHelper helper)
        {
            var output = new StringBuilder();
            output.Append(@"<div class=""TagCloud"">");

            using (var model = new UsersContext())
            {
                TagCloud tagCloud = model.GetTagCloud();

                foreach (MenuTag tag in tagCloud.MenuTags)
                {
                    output.AppendFormat(@"<div class=""tag{0}"">",
                                        tagCloud.GetRankForTag(tag));
                    output.Append(tag.Tag);
                    output.Append("</div>");
                }
            }

            output.Append("</div>");

            return output.ToString();
        }

        public static string TagRelationCloud(this HtmlHelper helper)
        {
            var output = new StringBuilder();
            output.Append(@"<div class=""TagCloud"">");

            using (var model = new UsersContext())
            {
                TagCloud tagCloud = model.GetTagRelationCloud();

                foreach (MenuTag tag in tagCloud.MenuTags)
                {
                    output.AppendFormat(@"<div class=""tag{0}"">",
                                        tagCloud.GetRankForTag(tag));
                    output.Append(tag.Tag);
                    output.Append("</div>");
                }
            }

            output.Append("</div>");

            return output.ToString();
        }

        
    }
}