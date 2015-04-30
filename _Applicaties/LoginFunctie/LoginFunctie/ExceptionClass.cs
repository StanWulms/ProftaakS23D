using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginFunctie
{
    class ExceptionsClass
    {

    }

    public class DatabaseConnectionFailed : Exception
    {
        public DatabaseConnectionFailed()
        { }

        public DatabaseConnectionFailed(string message)
            : base(message)
        { }
    }
}
