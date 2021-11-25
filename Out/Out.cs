using Out.Serialization;
using Out.TestClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer;

namespace Out
{
    public class Out
    {
        static void Main(string[] args)
        {
            ITracer timeTracer = new TimeTracer();
            TestClass _testClass = new TestClass(timeTracer);

            _testClass.InnerMethod1SleepTime = 50;
            _testClass.InnerMethod2SleepTime = 40;
            _testClass.AnotherInnerMethod1SleepTime = 60;
            _testClass.AnotherInnerMethod2SleepTime = 80;
            _testClass.AnotherThreadMethodSleepTime = 100;

            _testClass.StartMethod();
            TraceResult result = timeTracer.GetTraceResult();
            ISerializer serializer = new JsonResultSerializer();
            string resultXml = serializer.Serialize(result);
            //Writer.WriteData(Console.Out, resultXml);

            string writePath = "../result.txt";
            using (StreamWriter writer = new StreamWriter(writePath))
            {
                Writer.WriteData(writer, resultXml);
            }
        }
    }
}
