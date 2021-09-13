using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLibrary
{
   public class FileWriter : IWriter
    {

        private string filePath;

        public FileWriter(string filePath = "file.txt")
        {
            this.filePath = filePath;
        }

        public void Write(string text)
        {
            System.IO.File.WriteAllText(filePath, text);
        }
    }
}
