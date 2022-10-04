using System;
using System.Diagnostics;

namespace CommandHandlersMapping.Tests
{
    public interface ITest
    {
        public void WriteToOutput(string stuff);
    }

    public class MyTest : ITest
    {
        public void WriteToOutput(string stuff)
        {
            Console.WriteLine(stuff);
            Debug.WriteLine(stuff);
            Trace.WriteLine(stuff);
        }
    }
}
