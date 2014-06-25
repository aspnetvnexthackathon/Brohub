using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Brohub.Console;
using Microsoft.Framework.OptionsModel;
using Octokit;
using Octokit.Internal;

namespace Brohub.Analyzer
{
    public class GitDataProvider : IGitDataProvider
    {
	    public GitDataProvider(IOptionsAccessor<BrohubAnalyzerOptions> options)
	    {
            Token = options.Options.Token ?? "98fe272c4ff5c3bb834a8e645d10cfc25eae267e";
        }

        private string Token { get; set; }

        public async Task<IReadOnlyList<Commit>> GetCommitsAsync(string owner, string repo)
        {
            var client = new GitHubClient(new ProductHeaderValue("Brohub"))
            {
                Credentials = new Credentials(Token),
            };

            var connection = new Connection(
                new ProductHeaderValue("Brohub"),
                new Uri("https://api.github.com/"),
                new InMemoryCredentialStore(Credentials.Anonymous),
                new HttpClientAdapter(),
                new JsonNetSerializer());

            // Need to use private reflection to set this guy because their constructor doesn't work
            connection.GetType().InvokeMember(
                "_jsonPipeline", 
                BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Instance, 
                null, 
                connection, 
                new object[] { new JsonHttpPipeline(new JsonNetSerializer()) });

            var commitsClient = new AllCommitsClient(new ApiConnection(connection));
            var commits = await commitsClient.GetAllCommits(owner, repo);

            return commits.Select(c => c.Commit).ToList();
        }
    }
}