using System.IO;
using System.Web.Http;
using edu.stanford.nlp.ie.crf;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Diagnostics;
using Owin;

namespace StanfordNLPProject
{
    public class Startup
    {
        public static CRFClassifier Classifier { get; set; }
        public static CRFClassifier InitializeLibrary()
        {
            var startupPath = Path.GetFullPath(@"../../../NLPModel");
            var jarRoot = startupPath;
            var classifiersDirecrory = jarRoot + @"\classifiers";
            var classifier = CRFClassifier.getClassifierNoExceptions(
                classifiersDirecrory + @"\english.all.3class.distsim.crf.ser.gz");
            return classifier;
        }

        public static void Configuration(IAppBuilder app)
        {

            //app.Use(async (ctx, next) =>
            //{
            //    await ctx.Response.WriteAsync("NLP Server Initialized");
            //    await next.Invoke();
            //});
            app.Use(async (context, next) =>
            {
                // Add Header
                context.Response.Headers["Product"] = "Web Api Self Host";

                // Call next middleware
                await next.Invoke();
            });

            var config = new HttpConfiguration();

            app.MapSignalR();

            app.UseStaticFiles();

            ConfigureWebApi(app);
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();
            app.UseCors(CorsOptions.AllowAll);
            app.UseErrorPage(new ErrorPageOptions() { ShowExceptionDetails = true });
            Classifier = InitializeLibrary();
            app.UseWelcomePage();

        }


       private static void ConfigureWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional});
            app.UseWebApi(config);
        }
    }
}