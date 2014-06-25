using System.Collections.Generic;

namespace Brohub.Analyzer
{
    public class Result
    {
        public Result()
        {
            Items = new List<ResultItem>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public List<ResultItem> Items { get; set; }
    }

    public class ResultItem
    {
        public virtual string UserName { get; set; }
        public virtual object Value { get; set; }
    }
}