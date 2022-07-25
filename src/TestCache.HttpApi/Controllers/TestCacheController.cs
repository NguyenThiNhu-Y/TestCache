using TestCache.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace TestCache.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class TestCacheController : AbpController
    {
        protected TestCacheController()
        {
            LocalizationResource = typeof(TestCacheResource);
        }
    }
}