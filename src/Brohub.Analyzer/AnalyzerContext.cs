using System.Collections.Generic;

namespace Brohub.Analyzer
{
    public class AnalyzerContext
    {
        public AnalyzerContext(Repository repository)
        {
            Repository = repository;

            Datasources = new List<object>();
            Results = new List<Result>();
        }

        public List<object> Datasources { get; private set; }

        public Repository Repository { get; private set; }

        public List<Result> Results { get; private set; }
    }
}