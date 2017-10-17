using System;
using System.Diagnostics;

namespace BLL.Exceptions
{

    /// <summary>Exception that occurs when a database operation fails.</summary>
    class DatabaseOperationNotSupportedException : Exception
    {
        public DatabaseOperationNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
            // NOTE: Example of how we could trace exceptions (but probably not hw we'd actually do it in Azure)
            var ts = new TraceSource("BLL");
            ts.TraceInformation("DatabaseOperationNotSupportedException created: " + message);
        }
    }
}