﻿using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

namespace TracerLibrary
{
    class MethodTracer
    {
        private MethodResult curInfo;
        private Stopwatch stopwatch;

        public MethodTracer()
        {
            this.stopwatch = new Stopwatch();

            var traceTrace = new StackTrace();
            var stackFrame = traceTrace.GetFrame(1);
            string methodName = stackFrame.GetMethod().Name;
            string className = stackFrame.GetMethod().ReflectedType.Name;

            this.curInfo = new MethodResult(methodName,className);
        }

        public void StartTrace()
        {
            stopwatch.Start();
        }

        public void StopTrace()
        {
            stopwatch.Stop();
            var time = stopwatch.ElapsedMilliseconds;
            curInfo.setTime(time);
        }

        public MethodResult GetTraceResult()
        {
            return curInfo;
        }

        public void addChildResult(MethodResult methodResult)
        {
            this.curInfo.addChildMethod(methodResult);
        }

    }
}
