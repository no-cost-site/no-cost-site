using System.Threading.Tasks;
using NoCostSite.BusinessLogic.UpdateFilters;

namespace NoCostSite.BusinessLogic.Pages.Filters
{
    public class PageValidationFilter : UpdateFilterBase<Page>
    {
        public override Task BeforeUpsert(Page item) => Validate(item);

        private Task Validate(Page item)
        {
            return Task.CompletedTask;
        }
    }
}