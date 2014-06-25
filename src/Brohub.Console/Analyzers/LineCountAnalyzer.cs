
using System;
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
        public Dictionary<string, int> totalLines = new Dictionary<string, int>();
        public Dictionary<string, int> commentLines = new Dictionary<string, int>();
        public Dictionary<string, int> emptyLines = new Dictionary<string, int>();

        public Task AnalyzeAsync(AnalyzerContext context)
        {
            var localPath = context.Repository.LocalPath;
            using (var localRepo = new LibGit2Sharp.Repository(localPath))
            {
                Directory.SetCurrentDirectory(localPath);

                var files = Directory.EnumerateFiles(".", "*.cs", SearchOption.AllDirectories);

                foreach (var filex in files)
                {
                    string realFile = filex.Substring(2);

                    var text = File.ReadAllLines(realFile);
                    var status = localRepo.Index.RetrieveStatus(realFile);

                    if (status == FileStatus.Nonexistent || status == FileStatus.Untracked)
                    {
                        continue;
                    }

                    var blame = localRepo.Blame(realFile);

                    foreach (var hunk in blame)
                    {
                        UpdateBro(hunk, text);
                    }
                }
            }

            var allLinesResult = new Result()
            {
                Name = "LineCount",
                Description = "Number of lines modified",
                Items = totalLines
                    .OrderByDescending(kvp => kvp.Value)
                    .Select(kvp => new ResultItem()
                    {
                        UserName = kvp.Key,
                        Value = string.Format("changed {0} lines", kvp.Value),
                    }).ToList(),
            };

            context.Results.Add(allLinesResult);

            var commentLinesResult = new Result()
            {
                Name = "LineCount",
                Description = "Number of lines of comment commited",
                Items = commentLines
        .OrderByDescending(kvp => kvp.Value)
        .Select(kvp => new ResultItem()
        {
            UserName = kvp.Key,
            Value = string.Format("committed {0} lines of comments", kvp.Value),
        }).ToList(),
            };

            context.Results.Add(commentLinesResult);

            var emptyLinessResult = new Result()
            {
                Name = "LineCount",
                Description = "Number of empty lines committed",
                Items = emptyLines
                    .OrderByDescending(kvp => kvp.Value)
                    .Select(kvp => new ResultItem()
                    {
                        UserName = kvp.Key,
                        Value = string.Format("committed {0} empty lines", kvp.Value),
                    }).ToList(),
            };

            context.Results.Add(emptyLinessResult);

            return Task.FromResult<object>(null);
        }

        private void UpdateBro(BlameHunk hunk, string[] text)
        {
            var key = hunk.FinalCommit.Author.Name;
            if (key == null)
            {
                return;
            }

            if (totalLines.ContainsKey(key))
            {
                totalLines[key] += hunk.LineCount;
            }
            else
            {
                totalLines[key] = hunk.LineCount;
            }

            for (int lineNum = hunk.FinalStartLineNumber; lineNum < hunk.FinalStartLineNumber + hunk.LineCount; lineNum++)
            {
                var line = text[lineNum];

                if (line.Contains("//"))
                {
                    if (commentLines.ContainsKey(key))
                    {
                        commentLines[key] += 1;
                    }
                    else
                    {
                        commentLines[key] = 1;
                    }
                }
                else if (String.IsNullOrWhiteSpace(line))
                {
                    if (emptyLines.ContainsKey(key))
                    {
                        emptyLines[key] += 1;
                    }
                    else
                    {
                        emptyLines[key] = 1;
                    }
                }
            }
        }
    }
}
