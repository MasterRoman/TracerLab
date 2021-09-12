using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLibrary
{
    public interface ISerializer
    {

        string Serialize(TraceResult result);

    }
}
