using Xunit;
using System;
using System.Collections.Generic;
using TracerLibrary;
using System.Threading;

namespace TracerTests
{
    public class TracerTest 
    {

        private Tracer tracer;

        public TracerTest()
        {
            this.tracer = new Tracer();

        }

        private void TestMethod()
        {
            tracer.StartTrace();
            tracer.StopTrace();
        }

        [Fact]
        public void TestThreadId()
        {
            tracer.StartTrace();
            tracer.StopTrace();
            var result = tracer.GetTraceResult();
            Assert.Equal(Thread.CurrentThread.ManagedThreadId, result.threadResults[0].id);
        }

        [Fact]
        public void TestThreadCount()
        {
            var thread = new Thread(new ThreadStart(TestMethod));
            thread.Start();
            thread.Join();
            thread = new Thread(new ThreadStart(TestMethod));
            thread.Start();
            thread.Join();

            var result = tracer.GetTraceResult();

            Assert.Equal(2,result.threadResults.Count);
        }

        [Fact]
        public void TestMethodCount()
        {
            TestMethod();
            TestMethod();

            var result = tracer.GetTraceResult();
            var methodsCount = result.threadResults[0].methodsResult.Count;
            Assert.Equal(2, methodsCount);
        }

        [Fact]
        public void TestThreadAndSingleMethodTime()
        {
            TestMethod();

            var result = tracer.GetTraceResult();
            var time = result.threadResults[0].time;
            var methodTime = result.threadResults[0].methodsResult[0].time;
            Assert.Equal(methodTime, time);
        }

        [Fact]
        public void TestMethodAndClassName()
        {
            tracer.StartTrace();
            tracer.StopTrace();
            var result = tracer.GetTraceResult();
            
            Assert.Equal(nameof(TestMethodAndClassName), result.threadResults[0].methodsResult[0].name);

            Assert.Equal(nameof(TracerTest), result.threadResults[0].methodsResult[0].className);
        }

    }
}
