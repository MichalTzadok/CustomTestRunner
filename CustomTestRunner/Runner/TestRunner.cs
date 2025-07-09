using System;
using System.Reflection;
using CustomTestRunner.Discovery;
using CustomTestRunner.Execution;
using CustomTestRunner.Output;

namespace CustomTestRunner
{
    public class TestRunner
    {
        /// <summary>
        /// Main orchestrator class: discovers, runs, and logs tests.
        /// </summary>

        public void RunTests(Assembly assembly)
        {
            /// <summary>
            /// Main entry point for running the full test lifecycle:
            /// discovery → execution → output.
            /// </summary>
            /// <param name="assembly">Assembly containing test classes</param>

            var discoverer = new TestDiscoverer();
            var executor = new TestExecutor();
            var writer = new ResultWriter();

            var testClasses = discoverer.DiscoverTestClasses(assembly);
            var results = executor.ExecuteTests(testClasses);
            writer.Write(results);
        }
    }
}

