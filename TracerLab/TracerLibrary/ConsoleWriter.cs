using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLibrary
{
    class ConsoleWriter : IWriter
    {
        public void Write(string text)
        {
            Console.WriteLine(text);
        }
    }
}
