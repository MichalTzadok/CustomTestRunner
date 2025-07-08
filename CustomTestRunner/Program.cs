using System.Reflection;

namespace CustomTestRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new TestRunner();
            runner.RunTests(Assembly.GetExecutingAssembly());
        }
    }
}
