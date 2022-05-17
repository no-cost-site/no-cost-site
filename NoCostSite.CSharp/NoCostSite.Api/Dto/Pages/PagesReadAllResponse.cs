using System.Linq;
using NoCostSite.Api.Dto.Upload;
using NoCostSite.BusinessLogic.Pages;

namespace NoCostSite.Api.Dto.Pages
{
    public class PagesReadAllResponse
    {
        public PageItemDto[] Items { get; set; } = null!;

        public DirectoryDto Directory { get; set; } = null!;

        public static PagesReadAllResponse Ok(Page[] pages)
        {
            return new PagesReadAllResponse
            {
                Items = BuildPages(pages),
                Directory = BuildDirectory(pages),
            };
        }

        private static PageItemDto[] BuildPages(Page[] pages)
        {
            return pages
                .Select(x => new PageItemDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Url = x.Url,
                })
                .OrderBy(x => x.Name)
                .ToArray();
        }

        private static DirectoryDto BuildDirectory(Page[] pages)
        {
            var urls = pages
                .Select(x => x.Url)
                .ToArray();

            return DirectoryDto.Build(urls);
        }
    }
}