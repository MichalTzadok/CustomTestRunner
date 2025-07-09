using System;
using CustomTestRunner.Attributes;

namespace CustomTestRunner
{
    public class SampleTests
    {
        /// <summary>
        /// Sample test class demonstrating usage of [MyTest], [Setup], [Teardown].
        /// </summary>
        
        [Setup]
        public void Init()
        {
            Console.WriteLine("Setting up test...");
        }

        [MyTest]
        public void PassingTest()
        {
            Console.WriteLine("This test passes.");
        }

        [MyTest]
        public void FailingTest()
        {
            throw new Exception("This test fails.");
        }

        [MyTest]
        public void TestWithIntParameter(int x)
        {
            Console.WriteLine($"Received int: {x}");
            if (x != 0)
                throw new Exception("Expected 0 by default");
        }

        [MyTest]
        public void TestWithStringParameter(string msg)
        {
            Console.WriteLine($"Message: {msg}");
            if (msg != "")
                throw new Exception("Expected empty string by default");
        }

        [MyTest]
        public void TestWithBoolParameter(bool flag)
        {
            Console.WriteLine($"Boolean flag: {flag}");
        }

        [Teardown]
        public void Cleanup()
        {
            Console.WriteLine("Cleaning up after test...");
        }

    }
}
