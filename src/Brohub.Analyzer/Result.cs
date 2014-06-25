using System.Collections.Generic;

namespace Brohub.Analyzer
{
    public class Result
    {
        public string Name { get; set; }
        public string Description { get; set; }

        List<ResultItem> Items = new List<ResultItem>();
    }

    public class ResultItem
    {
        public virtual string UserName { get; set; }
        public virtual object Value { get; set; }
    }
}