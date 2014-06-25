using System.Collections.Generic;
using System.Threading.Tasks;

namespace Brohub.Analyzer
{
    public interface IAnalyzerEngine
    {
        Task<IEnumerable<Result>> AnalyzeAsync(Repository repository);
    }
}