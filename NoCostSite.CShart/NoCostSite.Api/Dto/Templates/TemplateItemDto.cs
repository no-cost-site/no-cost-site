using System;

namespace NoCostSite.Api.Dto.Templates
{
    public class TemplateItemDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
    }
}