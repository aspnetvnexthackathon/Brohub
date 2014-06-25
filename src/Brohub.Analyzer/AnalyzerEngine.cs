using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Framework.Runtime;

namespace Brohub.Analyzer
{
    public class AnalyzerEngine : IAnalyzerEngine
    {
        public Task<IEnumerable<Result>> AnalyzeAsync(Repository repository)
        {
            var projects = new ProjectsDatasource();
            var projectFiles = Directory.EnumerateFiles(repository.LocalPath, "project.json", SearchOption.AllDirectories);

            foreach (var projectFile in projectFiles)
            {
                Project project;
                if (Project.TryGetProject(projectFile, out project))
                {
                    projects.Projects.Add(project);
                }
            }

            var context = new AnalyzerContext(repository);
            context.Datasources.Add(projects);

            // TODO: call analyzers

            return Task.FromResult((IEnumerable<Result>)context.Results);
        }
    }
}