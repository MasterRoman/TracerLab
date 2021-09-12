using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLibrary
{
    public interface ITracer
    {
        void StartTrace();

        void StopTrace();

        TraceResult GetTraceResult();
    }
}
