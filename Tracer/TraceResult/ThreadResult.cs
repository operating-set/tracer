using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Tracer
{
    public class ThreadResult : BaseResult
    {
        private int _threadId;
        private long _executionTime;

        public ThreadResult()
        {
            _threadId = Thread.CurrentThread.ManagedThreadId;
            _methods = new List<MethodResult>();
        }

        [XmlAttribute("id")]
        public int ThreadId { 
            get 
            {
                return _threadId;
            }
            set
            {
                _threadId = value;
            }
        }

        [XmlAttribute("time")]
        public long ExecutionTime { 
            get
            {
                long executionTime = 0;
                Methods.ForEach(m => executionTime += m.ExecutionTime);
                _executionTime = executionTime;
                return executionTime;
            }
            set
            {
                _executionTime = value;
            }
        }

        public List<MethodResult> Methods { 
            get
            {
                return new List<MethodResult>(_methods);
            }
        }
        
    }
}
