using NoCostSite.BusinessLogic.Pages;

namespace NoCostSite.Api.Dto.Pages
{
    public class PagesReadResponse
    {
        public PageDto Page { get; set; } = null!;

        public static PagesReadResponse Ok(Page page)
        {
            return new PagesReadResponse
            {
                Page = new PageDto
                {
                    Id = page.Id,
                    TemplateId = page.TemplateId,
                    Name = page.Name,
                    Url = page.Url,
                    Title = page.Title,
                    Description = page.Description,
                    Keywords = page.Keywords,
                    Content = page.Content,
                },
            };
        }
    }
}