using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Xml;

namespace DatabaseHandler
{
    /// <summary>
    /// The class is responsible for handling all the calls to the different classes
    /// </summary>
    public abstract class RepositoryBase
    {
        /// <summary>
        /// The call field to the other classes
        /// </summary>
        protected Executor executor;


        /// <summary>
        /// Createsr a instance of Executor with a connectionString
        /// </summary>
        /// <param name="databaseName">The name of the database you want ot connect to</param>
        /// <param name="configFilePath">The absolute path to you connecingFile</param>
        public  RepositoryBase(string databaseName,string configFilePath)
        {
            string safe = GetConnectionString(databaseName, configFilePath);
            executor = new Executor(safe);
            
        }

        /// <summary>
        /// Finds the connectionString in the configurationfile
        /// </summary>
        /// <param name="databaseName">The name of the database you want ot connect to</param>
        /// <param name="configFilePath">The absolute path to you connecingfile</param>
        /// <returns></returns>
        private string GetConnectionString(string databaseName, string configFilePath)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(configFilePath);
            XmlNode xnodes = xdoc.SelectSingleNode("/configuration/connectionStrings");
            XmlNodeList elemlist = xdoc.GetElementsByTagName("add");
            foreach (XmlNode g in elemlist)
            {
                if (g.Attributes["name"].InnerText == $"{databaseName}")
                {
                    return g.Attributes["connectionString"].InnerText;
                }
            }
            return "";
        }
    }
}
