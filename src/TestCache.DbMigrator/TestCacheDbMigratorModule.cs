using TestCache.MongoDB;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace TestCache.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(TestCacheMongoDbModule),
        typeof(TestCacheApplicationContractsModule)
        )]
    public class TestCacheDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
