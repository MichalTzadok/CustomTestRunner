using System;
using CustomTestRunner.Attributes;

namespace CustomTestRunner
{
    public class SampleTests
    {
        [Setup]
        public void Setup1()
        {
            Console.WriteLine("Setup #1");
        }

        [Setup]
        public void Setup2()
        {
            Console.WriteLine("Setup #2");
        }

        [Teardown]
        public void Cleanup()
        {
            Console.WriteLine("Teardown");
        }

        [MyTest]
        public void Test1()
        {
            Console.WriteLine("Running Test1");
        }

        [MyTest]
        public void Test2()
        {
            Console.WriteLine("Running Test2");
            throw new Exception("Oops, something went wrong");
        }
    }
}
