using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using edu.stanford.nlp.ie.crf;
using Microsoft.Owin.Hosting;

namespace StanfordNLPProject
{
    public class Webserver
    {
        private IDisposable _webapp;
        public void Start()
        {

            _webapp = WebApp.Start<Startup>("http://localhost:8085");
       }

        public void Stop()
        {
            _webapp.Dispose();
        }

      
    }
}
