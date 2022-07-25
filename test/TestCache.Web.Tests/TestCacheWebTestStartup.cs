using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace TestCache
{
    public class TestCacheWebTestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<TestCacheWebTestModule>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.InitializeApplication();
        }
    }
}