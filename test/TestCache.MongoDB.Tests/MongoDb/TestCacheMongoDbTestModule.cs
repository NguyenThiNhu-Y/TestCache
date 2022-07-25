using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace TestCache.MongoDB
{
    [DependsOn(
        typeof(TestCacheTestBaseModule),
        typeof(TestCacheMongoDbModule)
        )]
    public class TestCacheMongoDbTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var stringArray = TestCacheMongoDbFixture.ConnectionString.Split('?');
                        var connectionString = stringArray[0].EnsureEndsWith('/')  +
                                                   "Db_" +
                                               Guid.NewGuid().ToString("N") + "/?" + stringArray[1];

            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = connectionString;
            });
        }
    }
}
