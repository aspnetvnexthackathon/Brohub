using System.Collections.Generic;
using System.Threading.Tasks;

namespace Brohub.Analyzer
{
    public interface IAnalyzer
    {
        Task AnalyzeAsync(AnalyzerContext context);
    }
}