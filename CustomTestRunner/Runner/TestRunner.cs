using System;
using System.Reflection;
using CustomTestRunner.Discovery;
using CustomTestRunner.Execution;
using CustomTestRunner.Output;

namespace CustomTestRunner
{
    public class TestRunner
    {
        public void RunTests(Assembly assembly)
        {
            var discoverer = new TestDiscoverer();
            var executor = new TestExecutor();
            var writer = new ResultWriter();

            var testClasses = discoverer.DiscoverTestClasses(assembly);
            var results = executor.ExecuteTests(testClasses);
            writer.Write(results);
        }
    }
}

