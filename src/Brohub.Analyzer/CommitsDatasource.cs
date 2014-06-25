using System.Collections.Generic;
using Octokit;

namespace Brohub.Analyzer
{
    public class CommitsDatasource
    {
	    public CommitsDatasource()
	    {
            Commits = new List<Commit>();
	    }
        
        public List<Commit> Commits { get; private set; }
    }
}