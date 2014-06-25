
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brohub.Analyzer;
using LibGit2Sharp;

namespace Brohub.Console.Analyzers
{
    public class LineCountAnalyzer : IAnalyzer
    {
        public Dictionary<string, int> values = new Dictionary<string, int>();

        public Task AnalyzeAsync(AnalyzerContext context)
        {
            var localPath = context.Repository.LocalPath;
            using (var localRepo = new LibGit2Sharp.Repository(localPath))
            {
                Directory.SetCurrentDirectory(localPath);

                var files = Directory.EnumerateFiles(".", "*", SearchOption.AllDirectories);

                foreach (var filex in files)
                {
                    string realFile = filex.Substring(2);

                    var status = localRepo.Index.RetrieveStatus(realFile);

                    if (status == FileStatus.Nonexistent || status == FileStatus.Untracked)
                    {
                        continue;
                    }

                    var blame = localRepo.Blame(realFile);

                    foreach (var hunk in blame)
                    {
                        Update(hunk);
                    }
                    // Do what you want with git stuff.
                }
            }

            var result = new Result()
            {
                Name = "LineCount",
                Description = "Number of lines modified",
                Items = values
                    .OrderByDescending(kvp => kvp.Value)
                    .Select(kvp => new ResultItem()
                    {
                        UserName = kvp.Key,
                        Value = string.Format("changed {0} lines", kvp.Value),
                    }).ToList(),
            };

            context.Results.Add(result);

            return Task.FromResult<object>(null);
        }

        public void Update(BlameHunk hunk)
        {
            var key = hunk.FinalCommit.Author.Name;
            if (key == null)
            {
                return;
            }

            if (values.ContainsKey(key))
            {
                values[key] += hunk.LineCount;
            }
            else
            {
                values[key] = hunk.LineCount;
            }
        }

        public string Dump()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var dude in values.OrderByDescending(k => k.Value))
            {
                builder.AppendLine(dude.Key + ": " + dude.Value);
            }

            return builder.ToString();
        }
    }
}
