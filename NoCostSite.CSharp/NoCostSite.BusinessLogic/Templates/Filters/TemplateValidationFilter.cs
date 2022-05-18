using System.Threading.Tasks;
using NoCostSite.BusinessLogic.Pages;
using NoCostSite.BusinessLogic.UpdateFilters;
using NoCostSite.BusinessLogic.Upload;
using NoCostSite.Utils;

namespace NoCostSite.BusinessLogic.Templates.Filters
{
    public class TemplateValidationFilter : UpdateFilterBase<Template>
    {
        public override Task BeforeUpsert(Template item) => Validate(item);
        
        private Task Validate(Template item)
        {
            Assert.Validate(() => item.Content.Contains(nameof(Page.Content).AsTag()), $"Template must contain tag {nameof(Page.Content).AsTag()}");
            
            return Task.CompletedTask;
        }
    }
}