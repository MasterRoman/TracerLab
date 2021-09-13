using System;
using System.Collections.Generic;
using System.Text;

using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace TracerLibrary
{
    public class XMLSerializer : ISerializer
    {
        public string Serialize(TraceResult result)
        {
            var threadsResults = result.threadResults;

            XDocument xDoc = new XDocument(new XElement("root"));

            foreach (ThreadResult threadResult in threadsResults)
            {
                var threadXElement = configureThreadXElement(threadResult);
                xDoc.Root.Add(threadXElement);
            }

            StringWriter stringWriter = new StringWriter();
            using (XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter))
            {
              //  xmlWriter.Formatting = Formatting.Indented;
                xDoc.WriteTo(xmlWriter);
            }
            return stringWriter.ToString();
        }

        private XElement configureThreadXElement(ThreadResult threadResult)
        {
            XElement threadXElement = new XElement("thread", new XAttribute("id", threadResult.id),
                                                            new XAttribute("time", threadResult.time)
                                                   );
            foreach (MethodResult methodResult in threadResult.methodsResult)
            {
                XElement methodXElement = configureMethodXElement(methodResult);
                threadXElement.Add(methodXElement);
            }
            return threadXElement;
        }

        private XElement configureMethodXElement(MethodResult methodResult)
        {
            XElement methodXElement = new XElement("method", new XAttribute("name", methodResult.name),
                                                            new XAttribute("className", methodResult.className),
                                                            new XAttribute("time", methodResult.time)
                                                   );
            foreach (MethodResult childMethodResult in methodResult.childMethodsResult)
            {
                if (childMethodResult.childMethodsResult.Count > 0)
                {
                    XElement childMethodXElement = configureMethodXElement(childMethodResult);
                    methodXElement.Add(childMethodXElement);
                }
            }
            return methodXElement;
        }


    }

}  

       
    


