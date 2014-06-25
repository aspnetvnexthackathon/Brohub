using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;

namespace Brohub.Analyzer
{
    public interface IGitDataProvider
    {
        Task<IReadOnlyList<Commit>> GetCommitsAsync(string owner, string repo);
    }
}