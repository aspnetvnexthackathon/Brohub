using System.Collections.Generic;

namespace Brohub
{
    public class RepositoryModel
    {
        public string url { get; set; }

        public IList<BadgeModel>  badges { get; set; }
    }
}