using System;

namespace NoCostSite.Api.Dto.Templates
{
    public class TemplateDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}