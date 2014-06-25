using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brohub.Analyzer
{
    public class AfterHoursCommitsAnalyzer : IAnalyzer
    {
        public Task AnalyzeAsync(AnalyzerContext context)
        {
            var commits = context.Datasources.OfType<CommitsDatasource>().Single();

            var commitsByAuthor = new Dictionary<string, int>();
            foreach (var commit in commits.Commits)
            {
                var time = commit.Commit.Committer.Date.TimeOfDay;
                if (new DateTime(2014, 6, 25, 23, 0, 0).TimeOfDay < time ||
                    new DateTime(2014, 6, 25, 7, 0, 0).TimeOfDay > time)
                {
                    var login = commit.Author.Login;

                    int count;
                    if (commitsByAuthor.TryGetValue(login, out count))
                    {
                        commitsByAuthor[login] = ++count;
                    }
                    else
                    {
                        commitsByAuthor[login] = 1;
                    }
                }
            }

            context.Results.Add(new Result());

            return Task.FromResult<object>(null);
        }
    }
}