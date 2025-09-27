using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UnitTests
{
    public abstract class TestBase
    {
        protected IServiceProvider ServiceProvider { get; private set; }
        public TestBase()
        {
            var hostBuilder = Host.CreateDefaultBuilder();

            hostBuilder.ConfigureServices(SetupServices);
            var builder = hostBuilder.Build();

            ServiceProvider = builder.Services;
            SetupData();
        }

        protected abstract void SetupServices(IServiceCollection services);
        protected abstract void SetupData();

    }
}