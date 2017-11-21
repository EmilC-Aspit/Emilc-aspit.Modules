using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace DatabaseHandler
{
    public static class ConfigurationManager
    {
        public static string GetConnectionString(string path, string name = null)
        {
           string fileName =  Directory.GetFiles(path).FirstOrDefault();
           string[] allLines = File.ReadAllLines(fileName);
           foreach(string text in allLines)
           {
                if (text.Contains(name))
                    return text;
           }
            return "";

        }
    }
}
