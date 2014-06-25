using Octokit;
using System;

namespace Brohub.Analyzer
{
    public class Brommit
    {
        public string Sha { get; set; }

        public Commit Commit { get; set; }

        public string Url { get; set; }

        public string HtmlUrl { get; set; }

        public string CommentsUrl { get; set; }

        public Author Author { get; set; }
    }
}