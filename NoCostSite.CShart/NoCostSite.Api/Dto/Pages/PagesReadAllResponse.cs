using System.Linq;
using NoCostSite.BusinessLogic.Pages;

namespace NoCostSite.Api.Dto.Pages
{
    public class PagesReadAllResponse
    {
        public PageItemDto[] Items { get; set; } = null!;

        public static PagesReadAllResponse Ok(Page[] pages)
        {
            return new PagesReadAllResponse
            {
                Items = pages
                    .Select(x => new PageItemDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                    })
                    .ToArray(),
            };
        }
    }
}