﻿using System.Linq;
using NoCostSite.BusinessLogic.Templates;

namespace NoCostSite.Api.Dto
{
    public class TemplatesReadAllResponse
    {
        public TemplateItemDto[] Items { get; set; } = null!;

        public static TemplatesReadAllResponse Ok(Template[] pages)
        {
            return new TemplatesReadAllResponse
            {
                Items = pages
                    .Select(x => new TemplateItemDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                    })
                    .ToArray(),
            };
        }
    }
}