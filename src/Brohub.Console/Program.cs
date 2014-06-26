using System;
using Brohub.Analyzer;
using Brohub.Console.Analyzers;
using Microsoft.Framework.DependencyInjection;
using Newtonsoft.Json;

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

            var outputJson = false;
            if (args.Length > 1 && args[1] == "--json")
            {
                outputJson = true;
            }

            Services = ServiceInitializer.Initialize();

            var gitCloneUrl = new Uri(args[0], UriKind.Absolute);
            var pathParts = gitCloneUrl.AbsolutePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            var owner = pathParts[0];
            var repoName = pathParts[1].Replace(".git", string.Empty);

            string gitClonePath = "./" + repoName;

            var initializer = new LocalRepoInitializer(gitCloneUrl.AbsoluteUri, gitClonePath);
            initializer.Initialize();

            var activator = Services.GetService<ITypeActivator>();

            var engine = activator.CreateInstance<AnalyzerEngine>(Services);

            var repository = new Analyzer.Repository()
            {
                LocalPath = gitClonePath,
                Owner = owner,
                RepoName = repoName,
            };

            var results = engine.AnalyzeAsync(repository).Result;

            if (outputJson)
            {
                System.Console.WriteLine(JsonConvert.SerializeObject(results));
            }
            else
            {
                foreach (var result in results)
                {
                    System.Console.WriteLine(result.Name);
                    System.Console.WriteLine(result.Description);
                    System.Console.WriteLine();

                    foreach (var item in result.Items)
                    {
                        System.Console.WriteLine("{0} - {1}", item.UserName, item.Value);
                    }

                    System.Console.WriteLine();
                    System.Console.WriteLine();

                    System.Console.WriteLine("Press ENTER to quit.");
                    System.Console.ReadLine();
                }
            }
        }
    }
}
