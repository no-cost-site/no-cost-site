using System;
using System.Threading.Tasks;
using NoCostSite.BusinessLogic.UpdateFilters;

namespace NoCostSite.BusinessLogic.Pages.Filters
{
    public class PagePatcherFilter : UpdateFilterBase<Page>
    {
        public override Task BeforeUpsert(Page item) => Patch(item);

        private Task Patch(Page item)
        {
            if (item.Id == Guid.Empty)
            {
                item.Id = Guid.NewGuid();
            }

            return Task.CompletedTask;
        }
    }
}