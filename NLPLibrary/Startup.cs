using System.IO;
using System.Web.Http;
using edu.stanford.nlp.ie.crf;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Diagnostics;
using Owin;

namespace NLPLibrary
{
    public class Startup
    {
        public static CRFClassifier Classifier { get; set; }
        public static void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            ConfigureWebApi(app);
            app.UseWelcomePage();
            config.EnsureInitialized();
            app.UseCors(CorsOptions.AllowAll);
            app.Use(async (ctx, next) => { await ctx.Response.WriteAsync("NLP Server Started"); });
            Classifier = InitializeLibrary();

        }
        public static CRFClassifier InitializeLibrary()
        {
            var startupPath = Path.GetFullPath(@"./App_Data");
            var jarRoot = startupPath;
            var classifiersDirecrory = jarRoot + @"\classifiers";
            var classifier = CRFClassifier.getClassifierNoExceptions(
                classifiersDirecrory + @"\english.all.3class.distsim.crf.ser.gz");
            return classifier;
        }
        private static void ConfigureWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            app.UseWebApi(config);
        }

    }
}