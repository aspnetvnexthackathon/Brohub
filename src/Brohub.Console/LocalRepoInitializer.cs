using System.IO;
using System.Management.Automation;
using LibGit2Sharp;

namespace Brohub
{
    public class LocalRepoInitializer
    {
        private string _gitCloneUrl;
        private string _gitClonePath;

        public LocalRepoInitializer(string gitCloneUrl, string gitClonePath)
        {
            _gitCloneUrl = gitCloneUrl;
            _gitClonePath = gitClonePath;
        }

        public void Initialize()
        {
            DeleteDirectory(_gitClonePath);

            Repository.Clone(_gitCloneUrl, _gitClonePath);
        }

        private static void DeleteDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                return;
            }

            var shell = PowerShell.Create();

            shell.AddScript("Remove-Item -Force -Recurse \"" + directory + "\"");

            shell.Invoke();
        }
    }
}
