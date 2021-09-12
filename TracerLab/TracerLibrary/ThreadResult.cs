using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLibrary
{
    struct ThreadResult
    {
        public int id { get; }
        public double time { get; private set; }

        public List<MethodResult> methodsResult { get; private set; }

    }
}
