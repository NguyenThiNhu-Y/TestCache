using System;
using System.Collections.Generic;
using System.Text;
using TestCache.Localization;
using Volo.Abp.Application.Services;

namespace TestCache
{
    /* Inherit your application services from this class.
     */
    public abstract class TestCacheAppService : ApplicationService
    {
        protected TestCacheAppService()
        {
            LocalizationResource = typeof(TestCacheResource);
        }
    }
}
