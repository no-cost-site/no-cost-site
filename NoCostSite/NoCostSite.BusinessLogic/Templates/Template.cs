using System;

namespace NoCostSite.BusinessLogic.Templates
{
    public class Template
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}