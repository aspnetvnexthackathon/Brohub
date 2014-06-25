using System;
using System.Linq;
using System.Threading.Tasks;

namespace Brohub.Analyzer
{
    /// <summary>
    /// Summary description for SampleRosalynAnalyzer
    /// </summary>
    public class SampleRosalynAnalyzer : IAnalyzer
    {
        public Task AnalyzeAsync(AnalyzerContext context)
        {
            var projects = context.Datasources.OfType<ProjectsDatasource>().Single();

            return Task.FromResult<object>(null);
        }
    }
}