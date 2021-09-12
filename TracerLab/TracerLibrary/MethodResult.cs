using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLibrary
{
    struct MethodResult
    {
        public string name { get; }
        public string className { get; }

        public double time { get; private set; }

        public List<MethodResult> childMethodsResult { get; private set; }
    }
}
