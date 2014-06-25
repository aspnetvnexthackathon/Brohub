using System;
using System.Linq;
using Brohub.Analyzer;
using Brohub.Analyzers;
using Microsoft.Framework.DependencyInjection;

namespace Brohub.Main
{
    public class Program
    {
        public static IServiceProvider Services { get; private set; }

        public static void Main(string[] args)
        {
            if (args.Length == 0 || !args[0].StartsWith("https://github.com/"))
            {
                System.Console.WriteLine("A GitHub clone url is required. Ex: https://github.com/aspnet/Mvc.git");
            }

            Services = ServiceInitializer.Initialize();
            string gitCloneUrl = args[0];
            string gitClonePath = "./" + gitCloneUrl.Split('/').Last().Replace(".git", string.Empty);

            //var initializer = new LocalRepoInitializer(gitCloneUrl, gitClonePath);

            //initializer.Initialize();

            var activator = Services.GetService<ITypeActivator>();

            var engine = activator.CreateInstance<AnalyzerEngine>(Services);

            var repository = new Analyzer.Repository()
            {
                Owner = "aspnet",
                RepoName = "Logging",
            };

            var results = engine.AnalyzeAsync(repository).Result;

            foreach (var result in results)
            {
                System.Console.WriteLine(result);
            }

            System.Console.WriteLine("Press ENTER to quit.");

            var analysis = new LineCountAnalyzer(gitClonePath);

            analysis.Run();

            System.Console.WriteLine(analysis.Dump());
        }
    }
}
