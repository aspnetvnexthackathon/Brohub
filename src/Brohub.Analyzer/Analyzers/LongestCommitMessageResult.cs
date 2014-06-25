using Octokit;

namespace Brohub.Analyzer
{
    public class LongestCommitMessageResult : Result
    {
        public Brommit Commit { get; set; }
        public int Length { get; set; }
    }
}