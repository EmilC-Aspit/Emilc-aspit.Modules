using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHandler
{
    public class DontBeStupidException : Exception
    {
        public DontBeStupidException() : base()
        {

        }

        public DontBeStupidException(string message) : base(message)
        {

        }

        public DontBeStupidException(string message, Exception innerException) : base(message,innerException)
        {

        }

        public DontBeStupidException(SerializationInfo info, StreamingContext context) : base(info,context)
        {

        }
    }
}
