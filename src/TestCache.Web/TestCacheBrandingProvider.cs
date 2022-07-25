using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace TestCache.Web
{
    [Dependency(ReplaceServices = true)]
    public class TestCacheBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "TestCache";
    }
}
