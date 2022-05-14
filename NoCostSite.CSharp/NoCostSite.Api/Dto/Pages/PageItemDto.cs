using System;

namespace NoCostSite.Api.Dto.Pages
{
    public class PageItemDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
    }
}