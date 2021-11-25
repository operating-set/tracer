using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Tracer
{
    public class MethodResult : BaseResult
    {
        private long _executionTime;

        [XmlAttribute("name")]
        public string MethodName { get; set; }
        [XmlAttribute("class")]
        public string ClassName { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public DateTime StartTime { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public DateTime StopTime { get; set; }
        [XmlAttribute("time")]
        public long ExecutionTime
        {
            get
            {
                _executionTime = StopTime.Subtract(StartTime).Milliseconds;
                return _executionTime;
            }
            set
            {
                _executionTime = value;
            }
        }

        public List<MethodResult> Methods
        {
            get
            {
                return new List<MethodResult>(_methods);
            }
        }

        public MethodResult(string methodName, string className)
        {
            _methods = new List<MethodResult>();
            MethodName = methodName;
            ClassName = className;
        }

        public MethodResult()
        {

        }

    }
}
