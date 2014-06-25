using System.Collections.Generic;
using Octokit;

namespace Brohub.Analyzer
{
    public class CommitsDatasource
    {
	    public CommitsDatasource()
	    {
            Commits = new List<Brommit>();
	    }
        
        public List<Brommit> Commits { get; private set; }
    }
}