using TestCache.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace TestCache.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class TestCachePageModel : AbpPageModel
    {
        protected TestCachePageModel()
        {
            LocalizationResourceType = typeof(TestCacheResource);
        }
    }
}