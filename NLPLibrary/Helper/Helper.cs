using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace NLPLibrary.Helper
{
    public static class Helper
    {
        public static List<string> FilterByEntityType(this string data, string entityType)
        {
            var regex = new Regex(entityType);
            var organization = new List<string>();
            foreach (var match in regex.Matches(data))
            {
                var v = regex.Match(match.ToString());
                var s = v.Groups[1].ToString();
                organization.Add(s);
            }
            return organization;
        }


        public static string ToEnumDescription(this Enum value)
        {
            var data =
                (DescriptionAttribute[])
                value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return data.Length > 0 ? data[0].Description : value.ToString();
        }

        public static string StripHtml(this string source)
        {
            var reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            return reg.Replace(source, "");
        }

        public static string GetOutputString(this KeyValuePair<string, List<string>> kvp)
        {
            var separator = ", ";
            var outputString = kvp.Key + ": " + "[" + string.Join(separator, kvp.Value) + "]";
            return outputString;
        }

        public static bool IsValidUrl(this string text)
        {
            var rx = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return rx.IsMatch(text);
        }
    }
}