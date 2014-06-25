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

            var longest = (Commit)null;
            foreach (var commit in commits.Commits)
            {
                if (commit.Message.Length > longest.Message.Length)
                {
                    longest = commit;
                }
            }

            context.Results.Add(new LongestCommitMessageResult()
            {
                Commit = longest,
                Length = longest.Message.Length,
            });

            return Task.FromResult<object>(null);
        }
    }
}