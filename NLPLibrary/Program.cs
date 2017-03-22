﻿using System;
using Microsoft.Owin.Hosting;
using NLPLibrary.Helper;
using Topshelf;

namespace NLPLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {

            StartTopshelf();
            //string uri = "http://localhost:8085";
            //using (WebApp.Start<Startup>(uri))
            //{
            //    var s2 = "CNN In news NIKE, Facebook that should come #cnn as little surprise to global air travelers, Singapore's done it again.For the fifth year in a row the city-state's Changi Airport"; // has been named world's best airport at the annual Skytrax World Airport Awards.The awards, announced during a ceremony in Amsterdam on March 14, are based on millions of international passenger surveys.How Singapore&#39;s airport is making its economy fly How Singapore's airport is making its economy flyAt this point, travelers might be wondering if there will ever be an airport that can surpass much - lauded Changi. Among the amenities spread through its three terminals are two 24 - hour movie theaters screening the latest blockbusters for free, a rooftop swimming pool and a butterfly garden.A fourth terminal has just been completed and is due to open in the second half of 2017. Winning the Skytrax World's Best Airport Award for the fifth consecutive year is immense encouragement to our 50,000-strong airport community at Changi Airport, every one of whom is passionate about delivering the most memorable airport experience to our passengers, said Lee Seow Hiang, CEO of Changi Airport Group, in a statement. Hamad International Airport: One to watch Though no airport has been able to knock Changi from the top spot, there's been a bit of movement in the rest of the top 10.Tokyo International Airport(Haneda) jumped two spots to push South Korea's Incheon International Airport from last year's second place position, moving Seoul's gateway to third.    Munich Airport dropped a spot and moved to fourth on the list, while Hong Kong International held onto its fifth place position.Doha, Qatar's Hamad International Airport moved up four spots to number six -- a particularly impressive shift considering it was 22nd on the list in 2015.Germany's Frankfurt Airport is the only newcomer to this year's World's Best Airports list, booting Osaka's Kansai International Airport from the top 10.No love for North America Once again, North America failed to have an airport in the top 10.Emirates named world &#39;s best airline in 2016 Skytrax awardsEmirates named world's best airline in 2016 Skytrax awardsVancouver Airport remains the No.1 airport in North America for an eighth consecutive year, moving up one spot to number 13 in this year's rankings.The top US airport in 2017 according to Skytrax is the Cincinnati / Northern Kentucky International Airport, number 26 on the list, followed by Denver International at number 28.For the full list of the worlds 100 best airports visit the World Airports Awards website.Skytrax handed out awards in a variety of other categories as well, covering everything from cleanliness and food to passenger service.Tokyo Haneda was voted the worlds cleanest airport, while Jakartas Soekarno--Hatta International Airport has been singled out as the most improved.When it comes to dining, Hong Kong leads the way, while Taiwan's Taoyuan got the top spot in the best airport staff category.In terms of shopping, there's no topping London Heathrow, according to Skytrax.Switch to a single dockerfile.In previous releases we had separate dockerfiles for debug and release.This provided the customization for the development experience, providing a fast iterative loop while developing, while providing an optimized image for production where startup time is a higher priority.With 0.40, weve switched to usingdocker-compose -f to pass additonal docker-compose.dev.debug|release.yml files. These additional compose files are passed in addition to the docker-compose.yml file to overlay additional settings, such as the volume maps for the course, debugger and nuget packages.Switched to using volume mapping for the BIN output, the debugger and the local nuget cache.A significant amount of time was spent copying in the context, debugger and restoring nuget packages within the image.By leveraging volume mounts, the iterative development experience is faster, leveraging the nugett cache and debugger on your development machine.This does mean the :dev image is empty.Remember to build a release image if you plan to push a locally built image to a remote host.Remove of the DockerTask.ps1 / sh files.We had initially added these script files to ease customization of our compile / build / compose workflows.These scripts became more complex than our initial intention.With.NET Core moving from xproj to csproj and MS build, we will leverage MS Build customization.Moved to optimized images. When building ASP.NET Core Web Applications, dockerfile FROM will reference themicrosoft/ aspnetcore:1.0.1 image.This image includes the ASP.NET Core Nuget packages, which have been pre - jitted improving startup performance.When building .NET Core Console Applications, dockerfile FROM will reference the microsoft / dotnet:1.0.1 image.Version 0.31.0 UpdatesRelease to support ASP.NET 5 RC1.Certificates created with earlier versions of the Visual Studio 2015 Tools for docker(v0.8 and below) are incompatible with the current version(v0.9).The latest implementation of Docker is more strict in how it handles the in domain names in certificates.Certificates that were previously generated with previous versions of tools in the.cloudapp.azure.com format are no longer accepted as the location of the VM host is not required(e.g. *.eastus.cloudapp.azure.com).Certificates may now only be re - used with VM in the same region.To use the v0.9 of the tools you have to delete the certificates in  % USERPROFILE %.docker and re - create your container hosts";
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

        private static void StartTopshelf()
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