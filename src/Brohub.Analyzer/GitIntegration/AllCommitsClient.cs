using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brohub.Console;
using Octokit;

namespace Brohub.Analyzer
{
    public class AllCommitsClient
    {
	    public AllCommitsClient(ApiConnection connection)
	    {
            Connection = connection;
            Pagination = new ApiPagination();
	    }

        private ApiConnection Connection { get; set; }

        private ApiPagination Pagination { get; set; }

        public async Task<IReadOnlyList<CommitBro>> GetAllCommits(string owner, string repo)
        {
            return await Connection.GetAll<CommitBro>(GetUri(owner, repo));
        }

        private Uri GetUri(string owner, string repo)
        {
            return new Uri(string.Format("repos/{0}/{1}/commits" , owner, repo), UriKind.Relative);
        }
    }
}