using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Common.Extensions
{
    public static class HtmlExtensions
    {
        /// <summary>
        /// Use line breaks for displaying descriptions
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString DisplayWithBreaksFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var model = html.Encode(metadata.Model).Replace("\r\n", "<br />\r\n");
            //model = Regex.Replace(model, "(.{" + 50 + "})", "$1" + "<br />");
            model = string.Join("<br />", model.Split()
    .Select((word, index) => new { word, index })
    .GroupBy(x => x.index / 15)
    .Select(grp => string.Join(" ", grp.Select(x => x.word))));

            if (String.IsNullOrEmpty(model))
                return MvcHtmlString.Empty;

            return MvcHtmlString.Create(model);
        }
    }
}
