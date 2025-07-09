using System.Reflection;

namespace CustomTestRunner
{
    class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// Initiates the test runner on the current assembly.
        /// </summary>

        static void Main(string[] args)
        {
            var runner = new TestRunner();
            runner.RunTests(Assembly.GetExecutingAssembly());
        }
    }
}
