using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHandler
{
    public class RepositoryBase : Executor
    {
        protected Executor executor;
        public RepositoryBase(string name) : base(name)
        {
            executor = new Executor(name);
        }
    }
}
