using Octokit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Brohub.Analyzer
{
    /// <summary>
    /// Summary description for LongestMessageAnalyzer
    /// </summary>
    public class LongestCommitMessageAnalyzer : IAnalyzer
    {
        public Task AnalyzeAsync(AnalyzerContext context)
        {
            var commits = context.Datasources.OfType<CommitsDatasource>().Single();

            var longest = (Brommit)null;
            foreach (var commit in commits.Commits)
            {
                if (longest == null ||
                    longest.Commit == null ||
                    commit.Commit.Message.Length > longest.Commit.Message.Length)
                {
                    longest = commit;
                }
            }

            context.Results.Add(new LongestCommitMessageResult()
            {
                Commit = longest,
                Length = longest.Commit.Message.Length,
            });

            return Task.FromResult<object>(null);
        }
    }
}