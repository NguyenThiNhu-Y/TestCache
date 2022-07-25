using TestCache.MongoDB;
using Xunit;

namespace TestCache
{
    [CollectionDefinition(TestCacheTestConsts.CollectionDefinitionName)]
    public class TestCacheDomainCollection : TestCacheMongoDbCollectionFixtureBase
    {

    }
}
