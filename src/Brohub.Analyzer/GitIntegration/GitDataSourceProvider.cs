using System.Threading.Tasks;

namespace Brohub.Analyzer
{
    public class GitDatasourceProvider : IAnalyzerDatasourceProvider
    {
        public GitDatasourceProvider(IGitDataProvider gitProvider)
        {
            GitProvider = gitProvider;
        }

        private IGitDataProvider GitProvider { get; set; }

        public async Task<object> GetDatasourceAsync(Repository repository)
        {
            var commitsDataSource = new CommitsDatasource();
            var commits = await GitProvider.GetCommitsAsync(repository.Owner, repository.RepoName);

            commitsDataSource.Commits.AddRange(commits);

            return commitsDataSource;
        }
    }
}