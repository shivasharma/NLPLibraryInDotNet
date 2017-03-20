using System;
using Microsoft.Owin.Hosting;
using StanfordNLPProject.Helper;
using Topshelf;

namespace StanfordNLPProject
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            StartTopshelf();
            //var uri = "http://localhost:8085";
            //using (WebApp.Start<Startup>(uri))
            //{
            //    var s2 =
            //        "CNN In news NIKE, Facebook that should come cnn as little surprise to global air travelers, Singapore's";
            //    Console.WriteLine("Started");
            //    Console.WriteLine("{0}\n", Startup.Classifier.classifyToString(s2, "tsv", true));
            //    //  Console.WriteLine("-----------------------------------------------------------");
            //    //  Console.WriteLine("{0}\n", Startup.Classifier.classifyToString(s2, "xml", true));
            //    Console.WriteLine("---------------------------------------------------------------");
            //    var classifierResult = Startup.Classifier.classifyWithInlineXML(s2);
            //    var newresult = classifierResult.FilterByEntityType(EntityType.Organization.ToEnumDescription());

            //    foreach (var entity in newresult)
            //        Console.WriteLine("Organizations:{0}\n", entity);

            //    Console.ReadKey();
            //    Console.WriteLine("Stopping");
            //}
        }
        static void StartTopshelf()
        {
            HostFactory.Run(x =>
            {
                x.Service<Webserver>(s =>
                {
                    s.ConstructUsing(name => new Webserver());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("This is a demo of a Windows Service using Topshelf.");
                x.SetDisplayName("Self Host Web API Demo");
                x.SetServiceName("AspNetSelfHostDemo");
            });
        }
    }
}