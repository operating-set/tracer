using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace Tracer
{
    public class TimeTracer : ITracer
    {

        private TraceResult _result;

        public TimeTracer()
        {
            _result = new TraceResult();
        }

        public TraceResult GetTraceResult()
        {
            return _result;
        }

        public void StartTrace()
        {
            StackTrace trace = new StackTrace();
            _result.AddMethod(trace);
        }

        public void StopTrace()
        {
            DateTime stopTime = DateTime.Now;
            StackTrace trace = new StackTrace();
            _result.StopTrace(trace, stopTime);
        }
    }
}
