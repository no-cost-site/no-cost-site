using System;
using System.Threading.Tasks;
using NoCostSite.BusinessLogic.UpdateFilters;

namespace NoCostSite.BusinessLogic.Templates.Filters
{
    public class TemplatePatcherFilter : UpdateFilterBase<Template>
    {
        public override Task BeforeUpsert(Template item) => Patch(item);

        private Task Patch(Template page)
        {
            if (page.Id == Guid.Empty)
            {
                page.Id = Guid.NewGuid();
            }

            return Task.CompletedTask;
        }
    }
}