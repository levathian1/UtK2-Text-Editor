using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UtK2_Text_Editor
{
    internal class CSVDumper
    {
        static string regexStr = "(.*([^\r\n ])*)[^\r\n ]";
        public static void writeToFile(string name, string text)
        {
            List<string> textMatches = new List<string>();
            List<string> bracketMatches = new List<string>();
            string name2 = name.Substring(0, name.Length - 2);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine($"{name.Substring(0, name.Length - 2)}.csv")))
            {
                foreach (Match m in Regex.Matches(text, regexStr, RegexOptions.IgnoreCase))
                {
                    outputFile.WriteLine(m.Value);
                }
            }
        }
    }
}
