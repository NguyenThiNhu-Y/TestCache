using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace TestCache.Data
{
    /* This is used if database provider does't define
     * ITestCacheDbSchemaMigrator implementation.
     */
    public class NullTestCacheDbSchemaMigrator : ITestCacheDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}