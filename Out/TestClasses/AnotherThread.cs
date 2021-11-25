using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tracer;

namespace Out.TestClasses
{
    public class AnotherThread
    {
        private ITracer _tracer;

        public int EndChainMethodSleepTime { get; set;  }

        public AnotherThread(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void AnotherThreadMethod()
        {
            _tracer.StartTrace();
            AnotherThreadInnerMethod();
            _tracer.StopTrace();
        }

        private void AnotherThreadInnerMethod()
        {
            _tracer.StartTrace();
            MethodWithoutTrace();
            _tracer.StopTrace();
        }

        private void MethodWithoutTrace()
        {
            EndChainMethod();
        }

        private void EndChainMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(EndChainMethodSleepTime);
            _tracer.StopTrace();
        }
    }
}
