using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace StanfordNLPProject.Helper
{
    public static class Helper
    {
        public static List<string> FilterByEntityType(this string data, string entityType)
        {
            var regex = new Regex(entityType);
            var entities = new List<string>();
            foreach (var match in regex.Matches(data))
            {
                var content = regex.Match(match.ToString());
                var entityResult = content.Groups[1].ToString();
                entities.Add(entityResult);
            }
            return entities;
        }

        public static string ToEnumDescription(this Enum value)
        {
            var data =
                (DescriptionAttribute[])
                value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return data.Length > 0 ? data[0].Description : value.ToString();
        }

        public  static string StripHtml( this string source)
        {
            var reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            return reg.Replace(source, "");
        }
    }
}