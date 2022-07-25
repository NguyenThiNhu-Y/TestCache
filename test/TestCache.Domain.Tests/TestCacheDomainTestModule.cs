using TestCache.MongoDB;
using Volo.Abp.Modularity;

namespace TestCache
{
    [DependsOn(
        typeof(TestCacheMongoDbTestModule)
        )]
    public class TestCacheDomainTestModule : AbpModule
    {

    }
}