using System.Linq;
using System.Threading.Tasks;
using NoCostSite.BusinessLogic.Templates;
using NoCostSite.BusinessLogic.UpdateFilters;
using NoCostSite.Utils;

namespace NoCostSite.BusinessLogic.Pages.Filters
{
    public class PageValidationFilter : UpdateFilterBase<Page>
    {
        private readonly PagesService _pagesService = new PagesService();
        private readonly TemplatesService _templatesService = new TemplatesService();
        
        public override Task BeforeUpsert(Page item) => Validate(item);

        private async Task Validate(Page page)
        {
            Assert.Validate(() => !string.IsNullOrEmpty(page.Name), "Name should be not empty");
            Assert.Validate(() => page.TemplateId != default, "Template should be not empty");
            Assert.Validate(() => !page.Url.StartsWith('/'), "Url should be not starts with '/'");
            Assert.Validate(() => !page.Url.EndsWith('/'), "Url should be not ends with '/'");
            Assert.Validate(() => !page.Url.StartsWith("_admin"), "Url should be not starts with '_admin'");

            var template = await _templatesService.Read(page.TemplateId);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            Assert.Validate(() => template != null, "Template not found");

            var pages = await _pagesService.ReadAll();
            Assert.Validate(() => !pages.Any(x => x.Url == page.Url && x.Id != page.Id), "There is another page with the specified url");
        }
    }
}