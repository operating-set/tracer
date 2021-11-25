using NUnit.Framework;
using Out;
using Out.Serialization;
using Out.TestClasses;
using System;
using System.IO;
using Tracer;

namespace Test
{
    public class TracerTest
    {
        private ITracer _tracer;
        private TestClass _testClass;
        private TraceResult _traceResult;

        [SetUp]
        public void Setup()
        {
            _tracer = new TimeTracer();
            _testClass = new TestClass(_tracer);

            _testClass.InnerMethod1SleepTime = 50;
            _testClass.InnerMethod2SleepTime = 40;
            _testClass.AnotherInnerMethod1SleepTime = 60;
            _testClass.AnotherInnerMethod2SleepTime = 80;
            _testClass.AnotherThreadMethodSleepTime = 100;

            _testClass.StartMethod();
            _traceResult = _tracer.GetTraceResult();
        }

        [Test]
        public void ResultNotNullTest()
        {
            Assert.NotNull(_traceResult);
        }

        [Test]
        public void ResultHasExactThreadsCountTest()
        {
            int expected = 2;
            int actual = _traceResult.Threads.Count;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ResultHasExactMethodsCountTest()
        {
            int expected = 9;
            int actual = _traceResult.Methods.Count;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ResultHasTracedMethodTest()
        {
            string tracedMethodName = "Method";
            MethodResult methodResult = _traceResult.GetMethodResult(tracedMethodName);
            Assert.NotNull(methodResult);
        }

        [Test]
        public void ResultMethodHasCorrectExecutionTimeTest()
        {
            long expected = _testClass.AnotherInnerMethod1SleepTime;
            long actual = _traceResult.GetMethodResult("AnotherInnerMethod1").ExecutionTime;
            Assert.AreEqual(expected, actual, 15.0);
        }

    }
}