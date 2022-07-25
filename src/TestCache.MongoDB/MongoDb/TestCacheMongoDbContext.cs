using MongoDB.Driver;
using TestCache.Books;
using TestCache.Users;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace TestCache.MongoDB
{
    [ConnectionStringName("Default")]
    public class TestCacheMongoDbContext : AbpMongoDbContext
    {
        public IMongoCollection<AppUser> Users => Collection<AppUser>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.Entity<AppUser>(b =>
            {
                /* Sharing the same "AbpUsers" collection
                 * with the Identity module's IdentityUser class. */
                b.CollectionName = "AbpUsers";
            });

            
        }

        public IMongoCollection<Book> Books => Collection<Book>();
    }
}
