using System;

namespace NoCostSite.ApiTests.Dto
{
    public class TemplateDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}