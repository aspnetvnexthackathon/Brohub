using System.Linq;
using System.Threading.Tasks;

namespace Brohub.Analyzer
{
    public class LongestCommitMessageAnalyzer : IAnalyzer
    {
        public Task AnalyzeAsync(AnalyzerContext context)
        {
            var commits = context.Datasources.OfType<CommitsDatasource>().Single();

            var longest = commits.Commits
                .Where(c => c.Commit != null && c.Commit.Message != null)
                .OrderByDescending(c => c.Commit.Message.Length);

            context.Results.Add(new Result()
            {
                Description = "Longest Commit Messages",
                Name = "LongestMessage",
                Items = longest.Take(10).Select(c => new ResultItem()
                {
                    UserName = c.Author.Login,
                    Value = string.Format("{0} chars", c.Commit.Message.Length),
                }).ToList(),
            });

            return Task.FromResult<object>(null);
        }
    }
}