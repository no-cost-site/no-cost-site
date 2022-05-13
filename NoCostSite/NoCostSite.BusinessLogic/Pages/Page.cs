using System;

namespace NoCostSite.BusinessLogic.Pages
{
    public class Page
    {
        public Guid Id { get; set; }
        
        public Guid TemplateId { get; set; }
        
        public string Name { get; set; } = null!;
        
        public string Url { get; set; } = null!;
        
        public string Title { get; set; } = null!;
        
        public string Description { get; set; } = null!;
        
        public string Keywords { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}