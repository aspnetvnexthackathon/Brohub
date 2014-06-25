using Octokit;
using System;
using System.Collections.Generic;

namespace Brohub.Console
{
    public class CommitBro
    {
        public string Sha { get; set; }
        public Commit  Commit { get; set; }
    }
}