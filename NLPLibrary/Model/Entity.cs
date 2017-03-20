using System.Collections.Generic;

namespace NLPLibrary.Model
{
    public class Entity
    {
        public string Rawtext { get; set; }
        public string Url { get; set; }
        public ICollection<string> Entities { get; set; }
    }
}