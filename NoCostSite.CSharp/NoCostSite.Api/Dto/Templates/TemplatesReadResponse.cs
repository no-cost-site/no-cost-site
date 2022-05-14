using NoCostSite.BusinessLogic.Templates;

namespace NoCostSite.Api.Dto.Templates
{
    public class TemplatesReadResponse
    {
        public TemplateDto Template { get; set; } = null!;

        public static TemplatesReadResponse Ok(Template page)
        {
            return new TemplatesReadResponse
            {
                Template = new TemplateDto
                {
                    Id = page.Id,
                    Name = page.Name,
                    Content = page.Content,
                },
            };
        }
    }
}