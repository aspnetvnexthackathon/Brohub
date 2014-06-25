using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Framework.Runtime;

namespace Brohub.Analyzer
{
    public class AnalyzerEngine : IAnalyzerEngine
    {
        public AnalyzerEngine(IEnumerable<IAnalyzer> analyzers, IEnumerable<IAnalyzerDatasourceProvider> datasources)
        {
            Analyzers = analyzers;
            Datasources = datasources;
        }

        private IEnumerable<IAnalyzer> Analyzers { get; set; }

        private IEnumerable<IAnalyzerDatasourceProvider> Datasources { get; set; }

        public async Task<IEnumerable<Result>> AnalyzeAsync(Repository repository)
        {
            var context = new AnalyzerContext(repository);

            foreach (var datasource in Datasources)
            {
                var data = await datasource.GetDatasourceAsync(repository);
                context.Datasources.Add(data);
            }

            foreach (var analyzer in Analyzers)
            {
                await analyzer.AnalyzeAsync(context);
            }

            return context.Results;
        }
    }
}