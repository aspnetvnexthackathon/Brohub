using System;
using System.IO;
using System.Linq;
using System.Management.Automation;
using LibGit2Sharp;

namespace Brohub
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || !args[0].StartsWith("https://github.com/"))
            {
                Console.WriteLine("A GitHub clone url is required. Ex: https://github.com/aspnet/Mvc.git");
            }

            string gitCloneUrl = args[0];
            string gitClonePath = "./" + gitCloneUrl.Split('/').Last().Replace(".git", string.Empty);

            DeleteDirectory(gitClonePath);

            Repository.Clone(gitCloneUrl, "./" + gitClonePath);

            using(var localRepo = new Repository(gitClonePath))
            {
                // Do what you want with git stuff.
            }

            Console.ReadLine();
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
