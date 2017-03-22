using System;
using Microsoft.Owin.Hosting;

namespace NLPLibrary
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