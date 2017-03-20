using System.ComponentModel;

namespace StanfordNLPProject.Helper
{
    public enum EntityType
    {
        [Description("<ORGANIZATION>(.*?)</ORGANIZATION>")] Organization = 1,

        [Description("<LOCATION>(.*?)</LOCATION>")] Location = 2,

        [Description("<PERSON>(.*?)</PERSON>")] Person = 3
    }
}