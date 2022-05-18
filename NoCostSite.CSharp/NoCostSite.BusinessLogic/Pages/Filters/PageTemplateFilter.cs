using System.Linq;
using System.Threading.Tasks;
using NoCostSite.BusinessLogic.Templates;
using NoCostSite.BusinessLogic.UpdateFilters;
using NoCostSite.Utils;

namespace NoCostSite.BusinessLogic.Pages.Filters
{
    public class PageTemplateFilter : UpdateFilterBase<Template>
    {
        private readonly PagesService _pagesService = new PagesService();

        public override Task BeforeDelete(Template item) => CheckNotExistPageWithTemplate(item);

        private async Task CheckNotExistPageWithTemplate(Template template)
        {
            var pages = await _pagesService.ReadAll();
            Assert.Validate(() => pages.All(x => x.TemplateId != template.Id), "There is a page with a template");
        }
    }
}