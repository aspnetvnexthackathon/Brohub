using Octokit;

namespace Brohub.Analyzer
{
    public class LongestCommitMessageResult : Result
    {
        public Commit Commit { get; set; }
        public int Length { get; set; }
    }
}