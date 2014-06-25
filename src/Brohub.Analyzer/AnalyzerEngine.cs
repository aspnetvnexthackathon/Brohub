using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Framework.Runtime;

namespace Brohub.Analyzer
{
    public class AnalyzerEngine : IAnalyzerEngine
    {
        public AnalyzerEngine(
            IGitDataProvider gitProvider,
            IEnumerable<IAnalyzer> analyzers)
        {
            GitProvider = gitProvider;
            Analyzers = analyzers;
        }

        private IEnumerable<IAnalyzer> Analyzers { get; set; }

        private IGitDataProvider GitProvider { get; set; }

        public async Task<IEnumerable<Result>> AnalyzeAsync(Repository repository)
        {
            var context = new AnalyzerContext(repository);

            var projectsDataSource = new ProjectsDatasource();
            var projectFiles = Directory.EnumerateFiles(repository.LocalPath, "project.json", SearchOption.AllDirectories);

            foreach (var projectFile in projectFiles)
            {
                Project project;
                if (Project.TryGetProject(projectFile, out project))
                {
                    projectsDataSource.Projects.Add(project);
                }
            }

            context.Datasources.Add(projectsDataSource);

            var commitsDataSource = new CommitsDatasource();
            var commits = await GitProvider.GetCommitsAsync(repository.Owner, repository.RepoName);

            commitsDataSource.Commits.AddRange(commits);

            context.Datasources.Add(commitsDataSource);

            foreach (var analyzer in Analyzers)
            {
                await analyzer.AnalyzeAsync(context);
            }

            return context.Results;
        }
    }
}