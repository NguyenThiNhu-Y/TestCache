using System.Threading.Tasks;

namespace TestCache.Data
{
    public interface ITestCacheDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
