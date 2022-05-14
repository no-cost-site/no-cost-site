using System;
using System.Collections.Generic;
using NoCostSite.BusinessLogic.Pages;
using NoCostSite.BusinessLogic.Templates;

namespace NoCostSite.BusinessLogic.FilesUpload
{
    public static class PagesBuilder
    {
        public static string Build(Page page, Template template)
        {
            return template.Content
                .Replace(nameof(Page.Title), page.Title)
                .Replace(nameof(Page.Description), page.Description)
                .Replace(nameof(Page.Keywords), page.Keywords)
                .Replace(nameof(Page.Content), page.Content);
        }

        public static string AsTag(this string tag) => $"<!-- {tag} -->";

        private static string Replace(this string str, string tag, string content)
        {
            return str.Contains(tag.AsTag())
                ? content.Replace(tag.AsTag(), TagBuilders[tag](content))
                : content;
        }

        private static readonly Dictionary<string, Func<string, string>> TagBuilders =
            new Dictionary<string, Func<string, string>>
            {
                {nameof(Page.Title), x => $"<title>{x}</title>"},
                {nameof(Page.Description), x => $@"<meta name=""description"" content=""{x}""/>"},
                {nameof(Page.Keywords), x => $@"<meta name=""Keywords"" content=""{x}""/>"},
                {nameof(Page.Content), x => x},
            };
    }
}