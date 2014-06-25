using Brohub.Analyzer;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Fallback;
using Microsoft.Framework.OptionsModel;
using System;
using System.Threading.Tasks;

namespace Brohub.GoodConsole
{
    public class Program
    {
        public Program(IServiceProvider services)
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

            serviceCollection.SetupOptions<BrohubAnalyzerOptions>((options) =>
            {
            });

            Services = serviceCollection.BuildServiceProvider(services);
        }

        public IServiceProvider Services { get; private set; }

        public async Task Main(string[] args)
        {
            var activator = Services.GetService<ITypeActivator>();

            var engine = activator.CreateInstance<AnalyzerEngine>(Services);

            var repository = new Repository()
            {
                Owner = "aspnet",
                RepoName = "Logging",
            };

            var results = await engine.AnalyzeAsync(repository);
            foreach (var result in results)
            {
                System.Console.WriteLine(result);
            }

            System.Console.WriteLine("Press ENTER to quit.");
            System.Console.ReadLine();
        }
    }
}
