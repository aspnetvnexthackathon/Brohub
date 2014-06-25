using System;
using Brohub.Analyzer;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Fallback;
using Microsoft.Framework.OptionsModel;

namespace Brohub
{
    public class ServiceInitializer
    {
        public static IServiceProvider Initialize()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<ITypeActivator, TypeActivator>();
            serviceCollection.Add(new ServiceDescriptor()
            {
                ServiceType = typeof(IOptionsAccessor<>),
                ImplementationType = typeof(OptionsAccessor<>),
                Lifecycle = LifecycleKind.Singleton,
            });
            serviceCollection.AddSingleton<IGitDataProvider, GitDataProvider>();
            serviceCollection.AddSingleton<IAnalyzerDatasourceProvider, GitDatasourceProvider>();
            serviceCollection.AddSingleton<IAnalyzer, LongestCommitMessageAnalyzer>();
            serviceCollection.AddSingleton<IAnalyzer, AfterHoursCommitsAnalyzer>();

            serviceCollection.SetupOptions<BrohubAnalyzerOptions>((options) =>
            {
            });

            return serviceCollection.BuildServiceProvider();
        }
    }
}
