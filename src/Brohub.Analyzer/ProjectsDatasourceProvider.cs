using System.IO;
using System.Threading.Tasks;
using Microsoft.Framework.Runtime;

namespace Brohub.Analyzer
{
    public class ProjectsDatasourceProvider : IAnalyzerDatasourceProvider
    {
        public Task<object> GetDatasourceAsync(Repository repository)
        {
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

            return Task.FromResult<object>(projectsDataSource);
        }
    }
}