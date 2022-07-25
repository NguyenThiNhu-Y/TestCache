using Volo.Abp.Modularity;

namespace TestCache
{
    [DependsOn(
        typeof(TestCacheApplicationModule),
        typeof(TestCacheDomainTestModule)
        )]
    public class TestCacheApplicationTestModule : AbpModule
    {

    }
}