using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHandler
{
    /// <summary>
    /// The class name and the class it inherts from
    /// </summary>
    public class RepositoryBase : Executor
    {
        /// <summary>
        /// The call field to the other classes
        /// </summary>
        protected Executor executor;

        /// <summary>
        /// Intialises the Executor and gives the written DBname to the other constructor
        /// </summary>
        /// <param name="name">the DatabaseName</param>
        public RepositoryBase(string name) : base(name)
        {
            executor = new Executor(name);
        }
    }
}
