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
        /// <summary>
        /// Gets the connectionString from a textfile
        /// </summary>
        /// <param name="path">The path is set to my documents</param>
        /// <param name="name">Name of the database you want to connect to</param>
        /// <returns></returns>
        public static string GetConnectionString(string path, string name = null)
        {
            string nnn = @"\ConnectionPath.config";
            if(!Directory.Exists(path))
            {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Config");
            }
            if(!File.Exists(path + nnn))
            {
                    File.Create(path + nnn);
                    Console.WriteLine("Created a Emptry File in My Documents/config put the connectionString there");
                    return null;
            }
            else
            {
                string[] fileName = File.ReadAllLines(path + nnn);
                foreach (string text in fileName)
                {
                    if (text.Contains(name))
                        return text;
                }
            }

            return "";

        }
    }
}
