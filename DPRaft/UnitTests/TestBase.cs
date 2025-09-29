using Core.BuildingBlocks.Messaging.Observer;
using Core.Infrastructure.Messaging.Observer;
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

        protected void SetupEventHandlers(IServiceCollection services)
        {
            services.AddSingleton<IEventObserver, EventObserver>();
            services.AddSingleton<IEventPublisher, EventPublisher>();
            services.AddSingleton<IEventDispatcher>(x => x.GetService<IEventObserver>() as EventObserver);
        }

    }
}