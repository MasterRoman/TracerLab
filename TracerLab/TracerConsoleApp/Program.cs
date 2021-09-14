using System;
using TracerLibrary;
using System.Threading;

namespace TracerConsoleApp
{
    class Program
    {
        public class Foo
        {
            private Bar bar;
            private ITracer tracer;

            internal Foo(ITracer tracer)
            {
                this.bar = new Bar(tracer);
                this.tracer = tracer;
            }

            public void MyMethod()
            {
                tracer.StartTrace();
                bar.InnerMethod();
                Thread.Sleep(15);
                bar.InnerMethod();
                tracer.StopTrace();
            }

            public void MyMethod2()
            {
                tracer.StartTrace();
                MyMethod();
                Thread.Sleep(10);
                tracer.StopTrace();
            }
        }

        public class Bar
        {
            private ITracer tracer;

            internal Bar(ITracer tracer)
            {
                this.tracer = tracer;
            }

            public void InnerMethod()
            {
                tracer.StartTrace();
                Thread.Sleep(300);
                InnerMethod2();
                tracer.StopTrace();
            }

            public void InnerMethod2()
            {
                tracer.StartTrace();
                Thread.Sleep(50);
                tracer.StopTrace();
            }
        }

        static void Main(string[] args)
        {

            Tracer tracer = new Tracer();
            Foo foo = new Foo(tracer);

            var thread = new Thread(foo.MyMethod);
            thread.Start();
            thread.Join();

            thread = new Thread(foo.MyMethod2);
            thread.Start();
            thread.Join();

            var res = tracer.GetTraceResult();

            var xmlSerializer = new XMLSerializer();
            var xml = xmlSerializer.Serialize(res);


            var jsonSerializer = new JsonSerializer();
            var json = jsonSerializer.Serialize(res);

            var writer = new ConsoleWriter();
            writer.Write(xml);
            writer.Write(json);


            var fileWriter = new FileWriter("xml.txt");
            fileWriter.Write(xml);

            fileWriter.changeFileName("json.txt");
            fileWriter.Write(json);

        }
    }
}
