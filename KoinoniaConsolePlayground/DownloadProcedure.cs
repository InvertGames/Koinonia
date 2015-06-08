using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Zip;
using Octokit;
using Octokit.Internal;


// 

namespace Koinonia
{


    class DownloadProcedure
    {
        public const string ACCESS_TOKEN = "6806f83caf50a0485f28100b56fea392b022166a";
        public const string CLIENT_SECRET = "60e3ed0894266317a2ba08890e42798f38073e6c";
        public const string CLIENT_ID = "c073eee3581af620de67";
        public const string TARGET_REPOSITORY_NAME = "SinisPrivateRepo";
        public const string TARGET_REPOSITORY_OWNER = "InvertGames";

        static void Main(string[] args)
        {

            var ConsoleTest = new DownloadProcedure();
            ConsoleTest.Test();
            Console.ReadLine();
        }

        public async void Test()
        {

            Console.WriteLine("Connecting Koinonia to Github...", TARGET_REPOSITORY_NAME);

            var github = new GitHubClient(new ProductHeaderValue("Koinonia"), new InMemoryCredentialStore(new Credentials("6806f83caf50a0485f28100b56fea392b022166a")));


            Console.WriteLine("Connected!");
            Console.WriteLine("Getting repository {0} by {1}...", TARGET_REPOSITORY_NAME, TARGET_REPOSITORY_OWNER);

            var repository = await github.Repository.Get(TARGET_REPOSITORY_OWNER, TARGET_REPOSITORY_NAME);

            Console.WriteLine("Getting uframepackage.json...");
            var content = await github.Repository.Content.GetAllContents(TARGET_REPOSITORY_OWNER, TARGET_REPOSITORY_NAME,
                "./");

            var archive = await github.Repository.Content.GetArchiveLink(TARGET_REPOSITORY_OWNER, TARGET_REPOSITORY_NAME, ArchiveFormat.Zipball);

            var request = HttpWebRequest.Create(archive);
            var response = await request.GetResponseAsync();
            var stream = response.GetResponseStream();


            Console.Write(archive);
            StreamReader reader = new StreamReader(new MemoryStream());

            using (ZipInputStream s = new ZipInputStream(stream))
            {

                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                { 



                    Console.WriteLine(theEntry.Name);

                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    // create directory
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(theEntry.Name))
                        {

                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            //


            //            Console.WriteLine("Repository obtained! Reading default branch '{0}'...", defaultBranchName);
            //            var branch = await github.Repository.GetBranch(TARGET_REPOSITORY_OWNER, TARGET_REPOSITORY_NAME, defaultBranchName);
            //            var commitRef = branch.Commit.Ref;
            //            var commit = await github.Repository.Commits.Get(TARGET_REPOSITORY_OWNER, TARGET_REPOSITORY_NAME, commitRef);
            //            Console.WriteLine("Last commmit by {0}, Url: {1}", commit.Committer, commit.Url);
            //            




        }
    }



}
