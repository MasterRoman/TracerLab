using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLibrary
{
    public struct TraceResult 
    {
        public IReadOnlyList<ThreadResult> threadResults { get; private set; }

        TraceResult(List<ThreadResult> threadResults)
        {
            this.threadResults = threadResults;
        }
    }
}
