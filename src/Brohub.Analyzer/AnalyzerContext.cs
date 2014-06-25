using System.Collections.Generic;

namespace Brohub.Analyzer
{
    public class AnalyzerContext
    {
        public AnalyzerContext(Repository respository)
        {
            Repository = Repository;

            Results = new List<Result>();
        }

        public Repository Repository { get; private set; }

        public List<Result> Results { get; private set; }
    }
}