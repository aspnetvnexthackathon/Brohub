using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LibGit2Sharp;

namespace Brohub.Analyzers
{
    public class LineCountAnalyzer
    {
        public Dictionary<string, int> values = new Dictionary<string, int>();
        private string gitClonePath;

        public LineCountAnalyzer(string path)
        {
            gitClonePath = path;
        }

        public void Run()
        {
            using (var localRepo = new Repository(gitClonePath))
            {
                Directory.SetCurrentDirectory(gitClonePath);

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
        }

        public void Update(BlameHunk hunk)
        {
            var key = hunk.FinalCommit.Author.Name;
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
