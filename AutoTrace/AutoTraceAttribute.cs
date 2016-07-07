using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoTrace
{
    public abstract class AutoTraceAttribute : Attribute
    {
        public Type TraceType { get; set; }

        public AutoTraceAttribute(Type traceType)
        {
            TraceType = traceType;
        }
    }
}

//https://docs.asp.net/en/latest/fundamentals/logging.html#implementing-logging-in-your-application