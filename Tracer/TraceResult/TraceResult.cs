using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tracer
{
    [Serializable]
    public class TraceResult
    {
        private object locker = new object();
        private Dictionary<int, ThreadResult> _threads;
        private Dictionary<MethodBase, MethodResult> _methods;


        public List<ThreadResult> Threads
        {
            get
            {
                return _threads.Values.ToList<ThreadResult>();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        public List<MethodResult> Methods
        {
            get
            {
                return _methods.Values.ToList<MethodResult>();
            }
        }

        public TraceResult()
        {
            _threads = new Dictionary<int, ThreadResult>();
            _methods = new Dictionary<MethodBase, MethodResult>();
        }

        public MethodResult GetMethodResult(string methodName)
        {
            foreach (KeyValuePair<MethodBase, MethodResult> entry in _methods)
            {
                MethodBase methodBase = entry.Key;
                if (methodBase.Name.Equals(methodName))
                {
                    return _methods.GetValueOrDefault(methodBase);
                }
            }
            throw new KeyNotFoundException("Failed to get MethodResult for method "); // todo insert method name
        }

        public void AddMethod(StackTrace trace)
        {
            lock(locker)
            {
                int threadId = Thread.CurrentThread.ManagedThreadId;
                if (!_threads.ContainsKey(threadId))
                {
                    ThreadResult newThreadResult = new ThreadResult();
                    _threads.Add(threadId, newThreadResult);
                }
                ThreadResult threadResult = _threads[threadId];
                StackFrame frame = trace.GetFrame(1);
                MethodBase methodInfo = frame.GetMethod();
                string methodName = methodInfo.Name;
                string className = methodInfo.DeclaringType.Name;
                MethodResult method = new MethodResult(methodName, className);
                _methods.Add(methodInfo, method);
                threadResult.AddMethod(trace, method);
                method.StartTime = DateTime.Now;
            }
        }

        public void StopTrace(StackTrace trace, DateTime stopTime)
        {
            StackFrame frame = trace.GetFrame(1);
            MethodBase method = frame.GetMethod();
            if (_methods.ContainsKey(method))
            {
                MethodResult result = _methods[method];
                result.StopTime = stopTime;
            }
        }

    }
}
