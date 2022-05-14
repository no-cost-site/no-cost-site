using System;

namespace NoCostSite.ApiTests.Dto
{
    public class PageItemDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
    }
}