using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tracer;

namespace Out.TestClasses
{
    public class TestClass
    {

        private ITracer _tracer;

        public int InnerMethod1SleepTime { get; set; }

        public int InnerMethod2SleepTime { get; set; }

        public int AnotherInnerMethod1SleepTime { get; set; }

        public int AnotherInnerMethod2SleepTime { get; set; }

        public int AnotherThreadMethodSleepTime { get; set; }

        public TestClass(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void StartMethod()
        {
            AnotherThread anotherThread = new AnotherThread(_tracer);
            anotherThread.EndChainMethodSleepTime = AnotherThreadMethodSleepTime;
            Thread thread = new Thread(new ThreadStart(anotherThread.AnotherThreadMethod));
            thread.Start();
            Method();
            AnotherMethod();
        }

        private void Method()
        {
            _tracer.StartTrace();
            InnerMethod1();
            InnerMethod2();
            _tracer.StopTrace();

        }

        private void AnotherMethod()
        {
            _tracer.StartTrace();
            AnotherInnerMethod1();
            AnotherInnerMethod2();
            _tracer.StopTrace();
        }

        private void InnerMethod1()
        {
            _tracer.StartTrace();
            Thread.Sleep(InnerMethod1SleepTime);
            _tracer.StopTrace();
        }

        private void InnerMethod2()
        {
            _tracer.StartTrace();
            Thread.Sleep(InnerMethod2SleepTime);
            _tracer.StopTrace();
        }

        private void AnotherInnerMethod1()
        {
            _tracer.StartTrace();
            Thread.Sleep(AnotherInnerMethod1SleepTime);
            _tracer.StopTrace();
        }

        private void AnotherInnerMethod2()
        {
            _tracer.StartTrace();
            Thread.Sleep(AnotherInnerMethod2SleepTime);
            _tracer.StopTrace();
        }
    }
}
