using System.Collections.Generic;
using Microsoft.Framework.Runtime;

namespace Brohub.Analyzer
{
    public class ProjectsDatasource
    {
	    public ProjectsDatasource()
	    {
            Projects = new List<Project>();
	    }

        public List<Project> Projects { get; private set; }
    }
}