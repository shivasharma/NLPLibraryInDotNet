using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using StanfordNLPProject.Helper;

namespace StanfordNLPProject.Model
{
    public class Entity
    {
        public string Rawtext { get; set; }
        public string Url { get; set; }
        public ICollection<string> Entities { get; set; }
    }
}